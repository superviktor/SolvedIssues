using System.Collections.Generic;
using FluentValidation;

namespace Validation.Api.Validator
{
    public static class CustomValidators
    {
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