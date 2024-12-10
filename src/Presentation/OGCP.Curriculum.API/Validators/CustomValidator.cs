using ArtForAll.Shared.Contracts.DDD;
using ArtForAll.Shared.ErrorHandler;
using FluentValidation;
using OGCP.Curriculums.Core.DomainModel;

namespace OGCP.Curriculums.API.Validators;

public static class CustomValidator
{
    public static IRuleBuilderOptions<T, TProperty> NotEmptyCustom<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return DefaultValidatorExtensions.NotEmpty(ruleBuilder)
            .WithMessage(Errors.Validation.ValueIsInvalid().Serialize());
    }

    public static IRuleBuilderOptions<T, string> LengthCustom<T>(this IRuleBuilder<T, string> ruleBuilder, int min, int max)
    {
        return DefaultValidatorExtensions.Length(ruleBuilder, min, max)
            .WithMessage(Errors.Validation.InvalidLength().Serialize());
    }

    public static IRuleBuilderOptionsConditions<T, IReadOnlyList<TElement>> ListMustContainNumberOfItems<T, TElement>(
        this IRuleBuilder<T, IReadOnlyList<TElement>> ruleBuilder, int? min = null, int? max = null)
    {
        return ruleBuilder.Custom((list, context) =>
        {
            if (min.HasValue && list.Count < min.Value)
            {
                context.AddFailure(Errors.Validation.CollectionIsTooSmall(min.Value, list.Count).Serialize());
            }
            if (max.HasValue && list.Count > max.Value)
            {
                context.AddFailure(Errors.Validation.CollectionIsTooLarge(min.Value, list.Count).Serialize());
            }
        });
    }

    public static IRuleBuilderOptions<T, TElement> MustBeEntity<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder, Func<TElement, Result<TValueObject, Error>> factoryMethod)
        where TValueObject : Entity
    {
        return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod(value);

            if (result.IsFailure)
            {
                context.AddFailure(result.Error.Serialize());
            }
        });
    }

    public static IRuleBuilderOptions<T, string> MustBeValueObject<T, TValueObject>(
        this IRuleBuilder<T, string> ruleBuilder, Func<string, Result<TValueObject, Error>> factoryMethod)
        where TValueObject : ValueObject
    {
        return (IRuleBuilderOptions<T, string>)ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod(value);

            if (result.IsFailure)
            {
                context.AddFailure(result.Error.Serialize());
            }
        });
    }

    public static IRuleBuilderOptions<T, int> MustBeValueObject<T, TValueObject>(
        this IRuleBuilder<T, int> ruleBuilder, Func<int, Result<TValueObject, Error>> factoryMethod)
        where TValueObject : ValueObject
    {
        return (IRuleBuilderOptions<T, int>)ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod(value);

            if (result.IsFailure)
            {
                context.AddFailure(result.Error.Serialize());
            }
        });
    }
}
