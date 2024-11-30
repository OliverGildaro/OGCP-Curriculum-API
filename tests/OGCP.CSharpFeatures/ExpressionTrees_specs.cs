using OGCP.Curriculum.API.domainmodel;
using System.Linq.Expressions;

namespace OGCP.CSharpFeatures;

public class ExpressionTrees_specs
{
    [Fact]
    public void Test1()
    {
        IEnumerable<Profile> profileList = new List<Profile>
        {
            QualifiedProfile.Create("Oliver", "CAstro", "I am bla", "Backedn").Value,
            QualifiedProfile.Create("Ivan", "CAstro", "I am bla", "Frontend").Value,
            QualifiedProfile.Create("Alvaro", "CAstro", "I am bla", "Architect").Value
        };
        //lets build the x > 1 expression
        var xExpression = Expression.Parameter(typeof(int), "x");
        var constantExpression = Expression.Constant(12);
        var greaterThan = Expression.GreaterThan(xExpression, constantExpression);

        var constant4Expression = Expression.Constant(4);
        var lessThan = Expression.LessThan(xExpression, constant4Expression);

        var or = Expression.Or(greaterThan, lessThan);

        //We need to pass the xExpression twice because the lambda needs its own argument to match the expression
        //So we can satisfied the Func<int, bool> signature
        Expression<Func<int, bool>> funcExpr = Expression.Lambda<Func<int, bool>>(or, false, new List<ParameterExpression> { xExpression });
        Func<int, bool> func = funcExpr.Compile();

        Assert.True(func(0));
        Assert.True(func(9));
    }

    [Fact]
    public void Test2()
    {
        IEnumerable<QualifiedProfile> profileList = new List<QualifiedProfile>
        {
            QualifiedProfile.Create("Oliver", "CAstro", "I am bla", "Backedn").Value,
            QualifiedProfile.Create("Ivan", "CAstro", "I am bla", "Frontend").Value,
            QualifiedProfile.Create("Alvaro", "CAstro", "I am bla", "Architect").Value
        };

        string firstName = "Oliver";
        string desiredJobRole = "Backedn";
        Expression? currentExpression = null;

        var parameter = Expression.Parameter(typeof(QualifiedProfile));
        //var constant = Expression.Constant(firstName);
        //var firstNameProp = Expression.Property(parameter, "FirstName");
        //var expression = Expression.Equal(firstNameProp, constant);
        currentExpression = CreateExpression<string>(firstName, currentExpression, "FirstName", parameter);


        //var constant2 = Expression.Constant(desiredJobRole);
        //var dsirJobRoleProp= Expression.Property(parameter, "DesiredJobRole");
        //var expression2 = Expression.Equal(dsirJobRoleProp, constant2);
        //var newExpre = Expression.And(expression, expression2);
        currentExpression = CreateExpression<string>(desiredJobRole, currentExpression, "DesiredJobRole", parameter);

        var funcExpr = Expression
            .Lambda<Func<QualifiedProfile, bool>>(currentExpression, false, new List<ParameterExpression> { parameter });
        var func = funcExpr.Compile();

        var profilesFOund = profileList.Where(func);

        Assert.True(profilesFOund.Any());
        Assert.Equal("Param_0 => ((Param_0.FirstName == \"Oliver\") And (Param_0.DesiredJobRole == \"Backedn\"))", funcExpr.ToString());
    }

    private static Expression CreateExpression<T>(T value, Expression? currentExpression, string propertyName, ParameterExpression objectParameter, string operatorType = "=")
    {
        var valueToTest = Expression.Constant(value);

        var propertyToCall = Expression.Property(objectParameter, propertyName);

        Expression operatorExpression;

        switch (operatorType)
        {
            case ">":
                operatorExpression = Expression.GreaterThan(propertyToCall, valueToTest);
                break;
            case "<":
                operatorExpression = Expression.LessThan(propertyToCall, valueToTest);
                break;
            case ">=":
                operatorExpression = Expression.GreaterThanOrEqual(propertyToCall, valueToTest);
                break;
            case "<=":
                operatorExpression = Expression.LessThanOrEqual(propertyToCall, valueToTest);
                break;
            default:
                operatorExpression = Expression.Equal(propertyToCall, valueToTest);
                break;
        }

        if (currentExpression == null)
        {
            currentExpression = operatorExpression;
        }
        else
        {
            var previousExpression = currentExpression;

            currentExpression = Expression.And(previousExpression, operatorExpression);
        }

        return currentExpression;
    }

    [Fact]
    private void CreateFactorial()
    {

        //int result
        var result = Expression.Variable(typeof(int), "result");
        //int value
        var value = Expression.Variable(typeof(int), "value");
        //int result = 1;
        var initializeResult = Expression.Assign(result, Expression.Constant(1));

        // Código generado: (se utiliza como punto de salida en un bucle)
        LabelTarget label = Expression.Label(typeof(int));

        // {
        //     result = result * value;
        //     value--;
        // }
        var multiplyAndDecrement = Expression.Block(
            Expression.Assign(result, Expression.Multiply(result, value)),
            Expression.PostDecrementAssign(value)
        );

        // if (value > 1)
        // {
        //     multiplyAndDecrement;
        // }
        // else
        // {
        //     break;
        // }
        var condition = Expression.IfThenElse(
            Expression.GreaterThan(value, Expression.Constant(1)),
            multiplyAndDecrement,
            Expression.Break(label, result)
        );

        // Código generado:
        // while (true)
        // {
        //     if (value > 1)
        //     {
        //         condition
        //     }
        //     else
        //     {
        //         label
        //     }
        // }
        var loop = Expression.Loop(condition, label);

        // Código generado:
        // {
        //     int result = 1;
        //     while (true)
        //     {
        //         if (value > 1)
        //         {
        //             result = result * value;
        //             value--;
        //         }
        //         else
        //         {
        //             break;
        //         }
        //     }
        //     return result;
        // }
        BlockExpression body = Expression.Block(
            new[] { result }, // Declaramos la variable result
            initializeResult, // Inicializamos result
            loop              // Ejecutamos el bucle
        );

        var lambda = Expression.Lambda<Func<int, int>>(body, value);

        // Compilar la expresión
        var func = lambda.Compile();

        Assert.Equal(6, func(3));
    }
}