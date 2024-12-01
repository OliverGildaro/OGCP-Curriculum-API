using OGCP.Curriculum.API.domainmodel;

namespace OGCP.CSharpFeatures.Helpers;

public class FakeRepo
{
    IQueryable<QualifiedProfile> profileList = new List<QualifiedProfile>
        {
            QualifiedProfile.Create("Oliver", "CAstro", "I am bla", "Backedn").Value,
            QualifiedProfile.Create("Ivan", "CAstro", "I am bla", "Frontend").Value,
            QualifiedProfile.Create("Alvaro", "CAstro", "I am bla", "Architect").Value
        }.AsQueryable();

    Dictionary<string, IOrderBy> objectExpre = new Dictionary<string, IOrderBy>
        {
            { "FirstName", new OrderBy<string>(x => x.FirstName)}
        };

    public IEnumerable<QualifiedProfile> Find(string orderBy)
    {
        var collection = profileList as IQueryable<QualifiedProfile>;
        return collection.OrderBy(objectExpre[orderBy]);
    }
}
