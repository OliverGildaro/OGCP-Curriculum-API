﻿using ArtForAll.Shared.ErrorHandler;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.services.interfaces
{
    public interface IProfileService : IService<Profile, int>
    {
        public Task<Result> AddLangue(int id, Language language);
        Task<Result> EdiLanguage(int id, Language language);
        Task<Result> RemoveLanguage(int id, int languageId);
    }
}