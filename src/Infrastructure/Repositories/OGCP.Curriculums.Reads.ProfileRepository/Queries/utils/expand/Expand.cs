using OGCP.Curriculum.API.DAL.Queries.Models;
using System.Dynamic;
using System.Linq.Expressions;

namespace OGCP.Curriculum.API.DAL.Queries.utils.expand;

public class Expand
{
    private static readonly Dictionary<string, Expression<Func<ProfileReadModel, object>>> SelectFields =
        new Dictionary<string, Expression<Func<ProfileReadModel, object>>>
        {
        { "FirstName", x => x.FirstName },
        { "LastName", x => x.LastName },
        { "Summary", x => x.Summary },
        { "Id", x => x.Id },
        { "CareerGoals", x => x.CareerGoals }
        };

    public static Expression<Func<ProfileReadModel, ProfileReadModel>> BuildProjection3(IEnumerable<string> fields)
    {
        // Code generated: x
        var parameter = Expression.Parameter(typeof(ProfileReadModel), "x");

        try
        {
            var bindings = fields
                .Where(SelectFields.ContainsKey)
                .Select(field =>
                {
                    //code generated: ??
                    var propertyExpression = Expression.Property(parameter, field);

                    var propertyInfo = typeof(ProfileReadModel).GetProperty(field);
                    if (propertyInfo == null)
                    {
                        throw new InvalidOperationException($"La propiedad '{field}' no se encontró en el modelo.");
                    }

                    return Expression.Bind(propertyInfo, propertyExpression);
                })
                .ToList();

            //Code generated: new ProfileReadmodel ??
            var newExpression = Expression.New(typeof(ProfileReadModel));
            var memberInit = Expression.MemberInit(newExpression, bindings);

            var lambda = Expression.Lambda<Func<ProfileReadModel, ProfileReadModel>>(memberInit, parameter);
            return lambda;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static Expression<Func<ProfileReadModel, ProfileReadModel>> BuildProjection4(IEnumerable<string> fields)
    {
        var parameter = Expression.Parameter(typeof(ProfileReadModel), "x");

        try
        {
            var bindings = fields
                .Where(SelectFields.ContainsKey)
                .Select(field =>
                {
                    if (field == "Languages")
                    {
                        // Crear expresión para propiedad de navegación "Languages"
                        var navigationProperty = typeof(ProfileReadModel).GetProperty(nameof(ProfileReadModel.Languages));
                        var propertyNavExpression = Expression.Property(parameter, navigationProperty);

                        // Proyección para propiedades individuales de LanguageReadModel
                        var languageParam = Expression.Parameter(typeof(LanguageReadModel), "l");
                        var languageProjection = Expression.MemberInit(
                            Expression.New(typeof(LanguageReadModel)),
                            typeof(LanguageReadModel).GetProperties()
                                .Select(p => Expression.Bind(p, Expression.Property(languageParam, p)))
                        );

                        var selectCall = Expression.Call(
                            typeof(Enumerable),
                            "Select",
                            new[] { typeof(LanguageReadModel), typeof(LanguageReadModel) },
                            propertyNavExpression,
                            Expression.Lambda(languageProjection, languageParam)
                        );

                        return Expression.Bind(navigationProperty, selectCall);
                    }
                    else if (field == "Educations")
                    {
                        // Crear expresión para propiedad de navegación "Educations"
                        var navigationProperty = typeof(ProfileReadModel).GetProperty(nameof(ProfileReadModel.Educations));
                        var propertyNavExpression = Expression.Property(parameter, navigationProperty);

                        // Proyección para propiedades individuales de EducationReadModel
                        var educationParam = Expression.Parameter(typeof(EducationReadModel), "e");
                        var educationProjection = Expression.MemberInit(
                            Expression.New(typeof(EducationReadModel)),
                            typeof(EducationReadModel).GetProperties()
                                .Select(p => Expression.Bind(p, Expression.Property(educationParam, p)))
                        );

                        var selectCall = Expression.Call(
                            typeof(Enumerable),
                            "Select",
                            new[] { typeof(EducationReadModel), typeof(EducationReadModel) },
                            propertyNavExpression,
                            Expression.Lambda(educationProjection, educationParam)
                        );

                        return Expression.Bind(navigationProperty, selectCall);
                    }

                    // Propiedades simples
                    var propertyInfo = typeof(ProfileReadModel).GetProperty(field);
                    var propertyExpression = Expression.Property(parameter, propertyInfo);
                    return Expression.Bind(propertyInfo, propertyExpression);
                })
                .ToList();

            var newExpression = Expression.New(typeof(ProfileReadModel));
            var memberInit = Expression.MemberInit(newExpression, bindings);

            return Expression.Lambda<Func<ProfileReadModel, ProfileReadModel>>(memberInit, parameter);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error al construir la proyección dinámica", ex);
        }
    }

}
