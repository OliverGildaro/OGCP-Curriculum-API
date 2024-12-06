using AutoMapper;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.DTOs.responses;

namespace OGCP.Curriculum.API.Mappers.responseMappers;

public class LanguageResponseMapper : Profile
{
    public LanguageResponseMapper()
    {
        this.CreateMap<LanguageReadModel, LanguageResponse>();
    }
}
