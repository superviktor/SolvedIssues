using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace Validation.Api.Validator
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, TElement> MustBeEntity<T, TElement, TEntity>(
            this IRuleBuilder<T, TElement> ruleBuilder, 
            Func<TElement, Result<TEntity>> factoryMethod)
            where TEntity : Entity
        {
            return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
            {
                var result = factoryMethod(value);
                if (result.IsFailure)
                    context.AddFailure(result.Error);
            });
        }
        public static IRuleBuilderOptions<T, string> MustBeValueObject<T, TValueObject>(
            this IRuleBuilder<T, string> ruleBuilder, 
            Func<string, Result<TValueObject>> factoryMethod)
            where TValueObject : ValueObject
        {
            return (IRuleBuilderOptions<T, string>) ruleBuilder.Custom((value, context) =>
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;
                var result = factoryMethod(value);
                if (result.IsFailure)
                    context.AddFailure(result.Error);
            });
        }

        public static IRuleBuilderOptionsConditions<T, IList<TElement>> ListMustContainsNumberOfElements<T, TElement>(
            this IRuleBuilder<T, IList<TElement>> ruleBuilder, int? min = null, int? max = null)
        {
            return ruleBuilder.Custom((list, context) =>
            {
                if(min.HasValue && list.Count < min.Value)
                    context.AddFailure("The list doesn't contains enough elements");

                if(max.HasValue && list.Count > max.Value)
                    context.AddFailure("The list contains too many elements");
            });
        }
    }
}