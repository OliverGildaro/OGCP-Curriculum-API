using OGCP.Curriculum.API.dtos;
using OGCP.Curriculum.API.models;

namespace OGCP.Curriculum.API.services
{
    public interface IProfileService<tEntity, tRequest>
        where tEntity : Profile
        where tRequest : Request
    {
        public tEntity Get();
        public void Create(tRequest request);
    }
}
