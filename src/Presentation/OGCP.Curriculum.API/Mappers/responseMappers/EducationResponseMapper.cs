using AutoMapper;
using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.DTOs.responses;

namespace OGCP.Curriculum.API.Mappers.responseMappers;

public class EducationResponseMapper : Profile
{
    public EducationResponseMapper()
    {
        this.CreateMap<EducationReadModel, EducationResponse>();
    }
}
