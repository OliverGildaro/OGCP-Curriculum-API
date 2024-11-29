using OGCP.Curriculum.API.DAL.Queries.Models;
using System.Linq.Expressions;

namespace OGCP.Curriculum.API.DAL.Queries.utils.expand
{
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

        public static Expression<Func<ProfileReadModel, object>> BuildProjection(IEnumerable<string> fields)
        {
            var parameter = Expression.Parameter(typeof(ProfileReadModel), "x");

            // Create bindings for each field in the selection
            try
            {
                var bindings = fields
                    .Where(field => SelectFields.ContainsKey(field)) // Asegúrate de que el campo es válido
                    .Select(field =>
                    {
                        var propertyExpression = SelectFields[field].Body; // Obtiene la expresión mapeada

                        // Buscar la propiedad correctamente en el tipo adecuado (ProfileReadModel o el tipo correspondiente)
                        var propertyInfo = typeof(ProfileReadModel).GetProperty(field);

                        if (propertyInfo == null)
                        {
                            throw new InvalidOperationException($"La propiedad '{field}' no se encontró en el modelo.");
                        }

                        // Crear un Binding para la propiedad con la expresión correspondiente
                        return Expression.Bind(propertyInfo, propertyExpression);
                    })
                    .ToList();

                // Construct a NewExpression for anonymous type or DTO
                var newExpression = Expression.New(typeof(ProfileReadModel));
                var memberInit = Expression.MemberInit(newExpression, bindings);

                return Expression.Lambda<Func<ProfileReadModel, object>>(memberInit, parameter);
            }

            catch (Exception ex)
            {

                throw;
            }


        }

    }
}
