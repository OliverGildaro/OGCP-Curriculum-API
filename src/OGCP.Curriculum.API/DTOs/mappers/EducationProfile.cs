using AutoMapper;
using OGCP.Curriculum.API.commanding.commands.AddEducationDegree;
using OGCP.Curriculum.API.commanding.commands.UpdateEducationToQualifiedProfile;
using OGCP.Curriculum.API.POCOS.requests.Education;

namespace OGCP.Curriculum.API.POCOS.mappers;

public class EducationProfile : Profile
{
    public EducationProfile()
    {
        this.UpdateEducationMapping();
        this.AddEducationMapping();
    }

    private void UpdateEducationMapping()
    {
        CreateMap<UpdateEducationRequest, UpdateEducationFromProfileCommand>()
            .Include<UpdateDegreeEducationRequest, UpdateEducationDegreeFromProfileCommand>()
            .Include<UpdateResearchEducationRequest, UpdateEducationResearchFromProfileCommand>()
            .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
            .ForMember(dest => dest.EducationId, opt => opt.Ignore());

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

    private void AddEducationMapping()
    {
        CreateMap<AddEducationRequest, AddEducationToQualifiedProfileCommand>()
            .Include<AddDegreeEducationRequest, AddDegreeEducationToQualifiedProfileCommand>()
            .Include<AddResearchEducationRequest, AddResearchEducationToQualifiedProfileCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<AddDegreeEducationRequest, AddDegreeEducationToQualifiedProfileCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Degree, opt => opt.MapFrom(src => src.Degree));

        CreateMap<AddResearchEducationRequest, AddResearchEducationToQualifiedProfileCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectTitle, opt => opt.MapFrom(src => src.ProjectTitle))
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
            .ForMember(dest => dest.Supervisor, opt => opt.MapFrom(src => src.Supervisor));
    }
}

