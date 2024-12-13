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
        //string key = $"{nameof(ProfileReadModel)}_collection_{parameters.FilterBy}_{parameters.SearchBy}_{parameters.PageNumber}_{parameters.PageSize}_{parameters.OrderBy}_{parameters.Desc}";

        //return memory.GetOrCreateAsync(key, entry =>
        //{
        //    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
        //    return decorated.FindAsync(parameters);
        //});
        return null;
    }

    public async Task<Maybe<ProfileReadModel>> FindAsync(int id)
    {
        string key = $"{nameof(ProfileReadModel)}{id}";
        string? value = await this.memory.GetStringAsync(key);

        if (value == null)
        {
            var profile = await this.decorated.FindAsync(id);

            if (profile.HasNoValue)
            {
                return null;
            }
            
            await memory
                .SetStringAsync(key, JsonConvert.SerializeObject(profile));
            
            return profile;
        }

        return JsonConvert.DeserializeObject<ProfileReadModel>(value);
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
