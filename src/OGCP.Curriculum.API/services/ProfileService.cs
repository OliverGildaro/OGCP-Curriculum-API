using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.factories;
using OGCP.Curriculum.API.models;
using OGCP.Curriculum.API.repositories;

namespace OGCP.Curriculum.API.services;

public class ProfileService<tEntity, tRequest> : IProfileService<tEntity, tRequest>
    where tEntity : Profile
    where tRequest : Request
{
    private readonly IProfileRepository<tEntity> repository;
    //private readonly IProfileFactory<tEntity> factory;

    public ProfileService(IProfileRepository<tEntity> repository)
    {
        this.repository = repository;
        //this.factory = factory;
    }

    public void Create(tRequest request)
    {
        //var profile = request switch
        //{
            //CreateGeneralProfileRequest _ => factory.CreateGeneral(request as CreateGeneralProfileRequest),
            //CreateQualifiedProfileRequest _ => factory.CreateQualified(request as CreateQualifiedProfileRequest),
            //CreateStudentProfileRequest _ => factory.CreateStudent(request as CreateStudentProfileRequest),
        //};

        //this.repository.Add(profile);
    }

    public tEntity Get()
    {
        return this.repository.FInd();
    }
}
