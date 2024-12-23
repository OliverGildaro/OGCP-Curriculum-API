using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.DAL.Mutations.Interfaces;
using OGCP.Curriculum.API.domainmodel;
using OGCP.Curriculum.API.services.interfaces;
using OGCP.Curriculums.AzureServices.BlobStorages;
using OGCP.Curriculums.Core.DomainModel.Images;
using OGCP.Curriculums.Core.DomainModel.valueObjects;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OGCP.Curriculum.API.services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileWriteRepo writeRepo;
        private readonly IBlobProfileImagesServices blobProfileImages;

        public ProfileService(IProfileWriteRepo writeRepo, IBlobProfileImagesServices blobProfileImages)
        {
            this.writeRepo = writeRepo;
            this.blobProfileImages = blobProfileImages;
        }

        public async Task<Result> AddImageAsync(int profileId, Image image, byte[] imageContent)
        {
            var @profile = await writeRepo.FindAsync(profileId);
            if (@profile.HasNoValue || !@profile.Value.AllowAddImageIsSuccess())
            {
                return Result.Failure("");
            }

            var resultAdd = @profile.Value.AddImage(image);

            if (resultAdd.IsFailure)
            {
                return resultAdd;
            }



            //imageBuffer
            var imageBuffResult = ImageBuffer.CreateNew(imageContent, image.ProfileId);
            if (imageBuffResult.IsFailure)
            {
                return Result.Failure(imageBuffResult.Error.Message);
            }

            var uploadResult = await this.blobProfileImages.UploadImageAsync(imageBuffResult.Value);

            if (uploadResult.IsFailure)
            {
                return Result.Failure(uploadResult.Message);
            }
            var result = await writeRepo.SaveChangesAsync();

            if (result < 1)
            {
                return Result.Failure("");
            }


            return Result.Success(uploadResult.Id);
        }

        public async Task<Result> AddLanguageSkillAsync(
            int profileId, int educationId, LanguageSkill skill)
        {
            Maybe<Profile> profile = await this.writeRepo.FindAsync(profileId);

            if (profile.HasNoValue)
            {
                return Result.Failure("");
            }
            var langAdded = profile.Value.AddLAnguageSkill(educationId, skill);

            if (langAdded.IsFailure)
            {
                return langAdded;
            }
            await this.writeRepo.SaveChangesAsync();
            return langAdded;
        }


        public async Task<Result> AddLangueAsync(int id, Language language)
        {
            Maybe<Profile> profile = await this.writeRepo.FindAsync(id);

            if (profile.HasNoValue)
            {
                return Result.Failure("");
            }
            var langAdded = profile.Value.AddLanguage(language);

            if (langAdded.IsFailure)
            {
                return langAdded;
            }
            await this.writeRepo.SaveChangesAsync();
            return langAdded;
        }

        public async Task<Result> CreateAsync(Profile request)
        {
            var result = this.writeRepo.Add(request);

            if (result.IsFailure)
            {
                throw new ArgumentException();
            }

            var resultSave = await writeRepo.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteProfile(int id)
        {
            if (!await this.writeRepo.ExistProfileAsync(id))
            {
                return Result.Failure("");
            }
            Maybe<Profile> profile = await this.writeRepo.FindAsync(id);

            if (profile.HasNoValue)
            {
                return Result.Failure("");
            }
            var result = this.writeRepo.DeleteProfileAsync(profile.Value);
            await this.writeRepo.SaveChangesAsync();

            return result;
        }

        public async Task<Result> EdiLanguageAsync(int id, int languageId, Language language)
        {
            Maybe<Profile> profile = await this.writeRepo.FindAsync(id);

            if (profile.HasNoValue)
            {
                return Result.Failure($"The profile id: {id}, not found");
            }
            Result result = profile.Value.EditLanguage(languageId, language);

            if (result.IsFailure)
            {
                return result;
            }
            await this.writeRepo.SaveChangesAsync();
            return result;
        }

        public Task<Maybe<Language>> FindByLanguageAsync(Language language)
        {
            return this.writeRepo.FindAsync(language);
        }

        public Task<IReadOnlyList<Profile>> GetAsync()
        {
            //return this.writeRepo.Find();
            return null;
        }

        public Task<Profile> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> RemoveLanguageAsync(int id, int languageId)
        {
            Maybe<Profile> profile = await this.writeRepo.FindAsync(id);

            Result result = profile.Value.RemoveLanguage(languageId);

            if (result.IsFailure)
            {
                return result;
            }
            await this.writeRepo.SaveChangesAsync();

            return result;
        }

        public async Task<Result> UpdateAsync(Profile profile)
        {
            Maybe<Profile> profileFound = await this.writeRepo.FindAsync(profile.Id);
            Result isSuccess = profileFound.Value.UpdateProfile(profile);
            await this.writeRepo.SaveChangesAsync();
            return isSuccess;
        }

        private Expression<Func<Profile, object>>[] GetQueryExpression()
        {
            return new Expression<Func<Profile, object>>[]
            {
                x => x.LanguagesSpoken,
                x => x.PersonalInfo,
            };
        }
    }
}
