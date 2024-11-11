using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.factories;
using OGCP.Curriculum.API.models;
using OGCP.Curriculum.API.repositories;

namespace OGCP.Curriculum.API.services;

public class ProfileService : Service<Profile, Request>, IProfileService
{
    public ProfileService(IProfileRepository repository, IProfileFactory factory)
        :base(repository, factory)
    {
    }
}
