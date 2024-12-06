using AutoMapper;
using OGCP.Curriculum.API.commanding.commands.CreateQualifiedProfile;
using OGCP.Curriculum.API.Commanding.commands.UpdateProfile;
using OGCP.Curriculum.API.DTOs.requests.Profile;
using OGCP.Curriculum.API.POCOS.requests.Profile;

namespace OGCP.Curriculum.API.DTOs.mappers;

public class ProfileRequestMapper :Profile
{
    public ProfileRequestMapper()
    {
        this.CreateProfileMapping();
        this.UpdateProfileMapping();
    }

    private void CreateProfileMapping()
    {
        this.CreateMap<ProfileRequest, CreateProfileCommand>()
            .Include<CreateGeneralProfileRequest, CreateGeneralProfileCommand>()
            .Include<CreateQualifiedProfileRequest, CreateQualifiedProfileCommand>()
            .Include<CreateStudentProfileRequest, CreateStudentProfileCommand>();

        this.CreateMap<CreateGeneralProfileRequest, CreateGeneralProfileCommand>();
        this.CreateMap<CreateQualifiedProfileRequest, CreateQualifiedProfileCommand>();
        this.CreateMap<CreateStudentProfileRequest, CreateStudentProfileCommand>();
    }

    private void UpdateProfileMapping()
    {
        this.CreateMap<UpdateProfileRequest, UpdateProfileCommand>()
            .Include<UpdateGeneralProfileRequest, UpdateGeneralProfileCommand>()
            .Include<UpdateQualifiedProfileRequest, UpdateQualifiedProfileCommand>()
            .Include<UpdateStudentProfileRequest, UpdateStudentProfileCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        this.CreateMap<UpdateGeneralProfileRequest, UpdateGeneralProfileCommand>();
        this.CreateMap<UpdateQualifiedProfileRequest, UpdateQualifiedProfileCommand>();
        this.CreateMap<UpdateStudentProfileRequest, UpdateStudentProfileCommand>();
    }
}
