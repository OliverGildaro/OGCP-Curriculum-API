using ArtForAll.Shared.ErrorHandler.Maybe;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using OGCP.Curriculum.API.DAL.Queries.interfaces;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.DAL.Queries.utils;
using OGCP.Curriculums.Reads.ProfileRepository.DTOs;
using System.Text.Json.Serialization;

namespace OGCP.Curriculums.Reads.ProfileRepository;

public class ProfileReadModelCacheRepository : IProfileReadModelRepository
{
    private readonly IProfileReadModelRepository decorated;
    private readonly IDistributedCache memory;

    public ProfileReadModelCacheRepository(IProfileReadModelRepository decorated, IDistributedCache memory)
    {
        this.decorated = decorated;
        this.memory = memory;
    }
    public Task<IReadOnlyList<ProfileReadModel>> FindAsync(QueryParameters parameters)
    {
        return decorated.FindAsync(parameters);
    }

    public async Task<Maybe<ProfileReadModel>> FindAsync(int id)
    {
        return await this.decorated.FindAsync(id);
        //string key = $"{nameof(ProfileReadModel)}{id}";
        //string? value = await this.memory.GetStringAsync(key);

        //if (value == null)
        //{

        //    if (profile.HasNoValue)
        //    {
        //        return null;
        //    }
            
        //    await memory
        //        .SetStringAsync(key, JsonConvert.SerializeObject(profile));
            
        //    return profile;
        //}

        //return JsonConvert.DeserializeObject<ProfileReadModel>(value);
    }

    public Task<IReadOnlyList<ProfileEducationDto>> FindEducationsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<EducationByRangeResponse> FindEducationsByRange(DateOnly startDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<EducationReadModel>> FindEducationsFromProfile(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<LanguageReadModel>> FindLanguagesFromProfile(int id)
    {
        throw new NotImplementedException();
    }

    public Task FindLanguagesGrouped()
    {
        throw new NotImplementedException();
    }
}
