using AutoMapper;
using OGCP.Curriculum.API.commanding.commands.UpdateEducationToQualifiedProfile;
using OGCP.Curriculum.API.POCOS.requests.Education;

namespace OGCP.Curriculum.API.POCOS.mappers;

public class EducationProfile : Profile
{
    public EducationProfile()
    {
        // Base mapping
        CreateMap<UpdateEducationRequest, UpdateEducationFromProfileCommand>()
            .Include<UpdateDegreeEducationRequest, UpdateEducationDegreeFromProfileCommand>()
            .Include<UpdateResearchEducationRequest, UpdateEducationResearchFromProfileCommand>()
            .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
            .ForMember(dest => dest.EducationId, opt => opt.Ignore());

        // Derived mappings
        CreateMap<UpdateDegreeEducationRequest, UpdateEducationDegreeFromProfileCommand>()
            .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
            .ForMember(dest => dest.EducationId, opt => opt.Ignore())
            .ForMember(dest => dest.Degree, opt => opt.MapFrom(src => src.Degree));

        CreateMap<UpdateResearchEducationRequest, UpdateEducationResearchFromProfileCommand>()
            .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
            .ForMember(dest => dest.EducationId, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectTitle, opt => opt.MapFrom(src => src.ProjectTitle))
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
            .ForMember(dest => dest.Supervisor, opt => opt.MapFrom(src => src.Supervisor));
    }
}

