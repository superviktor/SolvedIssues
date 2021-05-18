using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Validation.Api.Validator
{
    public class AddressesValidator : AbstractValidator<AddressDto[]>
    {
        public AddressesValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .Must(x => x?.Length >= 1 && x.Length < 2)
                .ForEach(a => 
                {
                    a.NotNull();
                    a.SetValidator(new AddressValidator());
                });
        }
    }
}
