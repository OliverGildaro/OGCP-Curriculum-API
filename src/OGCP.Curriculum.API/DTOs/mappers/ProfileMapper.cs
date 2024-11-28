using AutoMapper;
using OGCP.Curriculum.API.Commanding.commands.UpdateProfile;
using OGCP.Curriculum.API.DTOs.requests.Profile;

namespace OGCP.Curriculum.API.DTOs.mappers
{
    public class ProfileMapper :Profile
    {
        public ProfileMapper()
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
}
