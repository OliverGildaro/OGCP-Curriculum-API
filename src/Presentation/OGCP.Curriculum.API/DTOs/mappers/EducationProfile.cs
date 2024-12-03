using AutoMapper;
using OGCP.Curriculum.API.commanding.commands.AddEducationDegree;
using OGCP.Curriculum.API.commanding.commands.AddEducationResearch;
using OGCP.Curriculum.API.commanding.commands.UpdateEducationToQualifiedProfile;
using OGCP.Curriculum.API.Commanding.commands.RemoveEducationFromQualifiedProfile;
using OGCP.Curriculum.API.Commanding.commands.RemoveEducationFromStudentProfile;
using OGCP.Curriculum.API.DTOs.requests.Education;
using OGCP.Curriculum.API.POCOS.requests.Education;

namespace OGCP.Curriculum.API.POCOS.mappers;

public class EducationProfile : Profile
{
    public EducationProfile()
    {
        this.AddEducationMapping();
        this.UpdateEducationMapping();
        this.DeleteEducationMapping();
    }

    private void DeleteEducationMapping()
    {
        CreateMap<DeleteEducationRequest, RemoveEducationFromProfileCommand>()
            .Include<DeleteQualifiedEducationRequest, RemoveEducationFromQualifiedProfileCommand>()
            .Include<DeleteStudentEducationRequest, RemoveEducationFromStudentProfileCommand>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.EducationId, opt => opt.Ignore());

        CreateMap<DeleteQualifiedEducationRequest, RemoveEducationFromQualifiedProfileCommand>();
        CreateMap<DeleteStudentEducationRequest, RemoveEducationFromStudentProfileCommand>();
    }

    private void UpdateEducationMapping()
    {
        CreateMap<UpdateEducationRequest, UpdateEducationFromQualifiedProfileCommand>()
            .Include<UpdateDegreeEducationRequest, UpdateDegreeEducationFromQualifiedProfileCommand>()
            .Include<UpdateResearchEducationRequest, UpdateResearchEducationFromQualifiedProfileCommand>()
            .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
            .ForMember(dest => dest.EducationId, opt => opt.Ignore());

        CreateMap<UpdateDegreeEducationRequest, UpdateDegreeEducationFromQualifiedProfileCommand>()
            .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
            .ForMember(dest => dest.EducationId, opt => opt.Ignore())
            .ForMember(dest => dest.Degree, opt => opt.MapFrom(src => src.Degree));

        CreateMap<UpdateResearchEducationRequest, UpdateResearchEducationFromQualifiedProfileCommand>()
            .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
            .ForMember(dest => dest.EducationId, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectTitle, opt => opt.MapFrom(src => src.ProjectTitle))
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
            .ForMember(dest => dest.Supervisor, opt => opt.MapFrom(src => src.Supervisor));
    }

    private void AddEducationMapping()
    {
        CreateMap<AddEducationRequest, AddEducationToProfileCommand>()
            .Include<AddDegreeEducationRequest, AddDegreeEducationToQualifiedProfileCommand>()
            .Include<AddResearchEducationRequest, AddResearchEducationToQualifiedProfileCommand>()
            .Include<AddResearchEducationToStudentProfileRequest, AddEducationToStudentProfileCommand>()
            .ForMember(dest => dest.ProfileId, opt => opt.Ignore());

        CreateMap<AddDegreeEducationRequest, AddDegreeEducationToQualifiedProfileCommand>()
            .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
            .ForMember(dest => dest.Degree, opt => opt.MapFrom(src => src.Degree));

        CreateMap<AddResearchEducationRequest, AddResearchEducationToQualifiedProfileCommand>()
            .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectTitle, opt => opt.MapFrom(src => src.ProjectTitle))
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
            .ForMember(dest => dest.Supervisor, opt => opt.MapFrom(src => src.Supervisor));

        CreateMap<AddResearchEducationToStudentProfileRequest, AddEducationToStudentProfileCommand>()
            .ForMember(dest => dest.ProfileId, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectTitle, opt => opt.MapFrom(src => src.ProjectTitle))
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
            .ForMember(dest => dest.Supervisor, opt => opt.MapFrom(src => src.Supervisor));
    }
}

