using OGCP.Curriculum.API.DAL.Queries.Models;
using System.Dynamic;
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

        public static Expression<Func<ProfileReadModel, ProfileReadModel>> BuildProjection3(IEnumerable<string> fields)
        {
            // Paso 1: Crear un parámetro de entrada para la expresión lambda
            var parameter = Expression.Parameter(typeof(ProfileReadModel), "x");

            try
            {
                // Paso 2: Crear una lista para inicializar las propiedades seleccionadas
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
                    })
                    .ToList();

                // Paso 3: Crear expresión para instanciar `ProfileReadModel`
                var newExpression = Expression.New(typeof(ProfileReadModel));
                var memberInit = Expression.MemberInit(newExpression, bindings);

                // Paso 4: Crear lambda que devuelve una instancia de `ProfileReadModel`
                var lambda = Expression.Lambda<Func<ProfileReadModel, ProfileReadModel>>(memberInit, parameter);
                return lambda;
            }
            catch (Exception)
            {
                throw;
            }
        }


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
}
