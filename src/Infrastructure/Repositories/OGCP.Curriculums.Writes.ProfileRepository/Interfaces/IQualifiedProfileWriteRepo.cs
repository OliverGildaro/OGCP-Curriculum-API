using ArtForAll.Shared.ErrorHandler;
using ArtForAll.Shared.ErrorHandler.Maybe;
using OGCP.Curriculum.API.domainmodel;

namespace OGCP.Curriculum.API.DAL.Mutations.Interfaces;

public interface IQualifiedProfileWriteRepo : IWriteRepository<QualifiedProfile, int>
{
    Task<Maybe<DegreeEducation>> FindDegreeEducation(string institution, EducationLevel degree);
    Task<Maybe<DegreeEducation>> FindDegreeEducation(DegreeEducation education);
    Task<Maybe<ResearchEducation>> FindResearchEducation(string institution, string projectTitle);
    Task<Result> RemoveOrphanEducationsAsync(string removeEducation);

}
