using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.factories.interfaces;
using OGCP.Curriculum.API.models;
using OGCP.Curriculum.API.repositories.interfaces;
using OGCP.Curriculum.API.services.interfaces;

namespace OGCP.Curriculum.API.services;

public class GeneralProfileService : Service<Profile, ProfileRequest>, IProfileService
{
    public GeneralProfileService(IGeneralProfileRepository repository, IProfileFactory factory)
        :base(repository, factory)
    {
    }
}
