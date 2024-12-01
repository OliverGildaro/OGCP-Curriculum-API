using OGCP.Curriculum.API.DAL.Queries.Models;
using OGCP.Curriculum.API.domainmodel;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Reflection;
using System.Reflection.Metadata;
using System.Data.Common;

namespace OGCP.CSharpFeatures.Helpers.Order;

public class FakeRepo
{
    IQueryable<QualifiedProfile> profileList = new List<QualifiedProfile>
        {
            QualifiedProfile.Create("Oliver", "CAstro", "I am bla", "Backedn").Value,
            QualifiedProfile.Create("Ivan", "CAstro", "I am bla", "Frontend").Value,
            QualifiedProfile.Create("Alvaro", "CAstro", "I am bla", "Architect").Value
        }.AsQueryable();

    IQueryable<ProfileReadModel> profileReadList = new List<ProfileReadModel>
        {
            new ProfileReadModel
            {
                FirstName="Oliver",
                LastName="Castro",
                Summary="Backend"
            },
            new ProfileReadModel
            {
                FirstName="Alvaro",
                LastName="Castro",
                Summary="Backend"
            },
            new ProfileReadModel
            {
                FirstName="Ivan",
                LastName="Castro",
                Summary="Backend"
            },
        }.AsQueryable();


    Dictionary<string, IOrderBy> objectExpre = new Dictionary<string, IOrderBy>
        {
            { "FirstName", new OrderBy<string>(x => x.FirstName)}
        };

    public IEnumerable<QualifiedProfile> Find(string orderBy)
    {
        return profileList.OrderBy(objectExpre[orderBy]);
    }

    public List<ExpandoObject> Find(string[] fields)
    {
        if (fields == null || fields.Length == 0)
        {
            throw new ArgumentNullException();
        }
        var projection = Expand.BuildProjection2(fields);

        var result = this.profileReadList.Select(projection).ToList();

        return result;
    }
}

public class Expand
{
    private static readonly Dictionary<string, Expression<Func<ProfileReadModel, object>>> SelectFields =
        new Dictionary<string, Expression<Func<ProfileReadModel, object>>>
        {
            { "FirstName", x => x.FirstName },
            { "LastName", x => x.LastName },
            { "Summary", x => x.Summary },
            { "CareerGoals", x => x.CareerGoals }
        };

    public static Func<ProfileReadModel, ExpandoObject> BuildProjection2(IEnumerable<string> fields)
    {
        var parameter = Expression.Parameter(typeof(ProfileReadModel), "x");

        var bindings = fields
            .Where(SelectFields.ContainsKey)
            .Select(field =>
            {
                // Obtener expresión para el campo
                var propertyExpression = Expression.Property(parameter, field);

                // Obtener propiedad del modelo
                var propertyInfo = typeof(ProfileReadModel).GetProperty(field);
                if (propertyInfo == null)
                {
                    throw new InvalidOperationException($"La propiedad '{field}' no se encontró en el modelo.");
                }

                // Generar el binding para la propiedad
                return Expression.Bind(propertyInfo, propertyExpression);
            }).ToList();
        
        return (ProfileReadModel x) =>
        {
            // Crear un ExpandoObject
            var expando = new ExpandoObject() as IDictionary<string, object>;

            // Asignar los valores de los campos seleccionados al ExpandoObject
            foreach (var binding in bindings)
            {
                // Obtener el valor de la propiedad usando la expresión
                var valueExpression = binding.Expression;
                var value = Expression.Lambda(valueExpression, parameter).Compile().DynamicInvoke(x);

                // Agregar la propiedad al ExpandoObject
                expando[binding.Member.Name] = value;
            }

            return (ExpandoObject)expando;
        };
    }
}


