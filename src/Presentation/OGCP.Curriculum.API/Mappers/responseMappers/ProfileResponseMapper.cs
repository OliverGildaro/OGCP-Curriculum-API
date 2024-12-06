using AutoMapper;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.POCOS.responses;

namespace OGCP.Curriculum.API.Mappers.responseMappers;

public class ProfileResponseMapper : Profile
{
    public ProfileResponseMapper()
    {
        this.CreateMap<ProfileReadModel, ProfileResponse>()
            .ForMember(dest => dest.Languages,
                opt => opt.MapFrom(src => src.ProfileLanguages.Select(pl => pl.Language)))
            .ForMember(dest => dest.Educations,
                opt => opt.MapFrom(src => src.ProfileEducations.Select(pe => pe.Education)));
    }
}
