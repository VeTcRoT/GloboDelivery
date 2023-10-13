using FluentValidation;

namespace GloboDelivery.Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(a => a.Country)
                .NotNull().WithMessage("{PropertyName} should not be null.")
                .MinimumLength(3).WithMessage("{PropertyName} length should be greater or equal to 3.")
                .MaximumLength(20).WithMessage("{PropertyName} length should be less or equal to 20.");

            RuleFor(a => a.City)
                .NotNull().WithMessage("{PropertyName} should not be null.")
                .MinimumLength(3).WithMessage("{PropertyName} length should be greater or equal to 3.")
                .MaximumLength(20).WithMessage("{PropertyName} length should be less or equal to 20.");

            RuleFor(a => a.AdministrativeArea)
                .NotNull().WithMessage("{PropertyName} should not be null.")
                .MinimumLength(5).WithMessage("{PropertyName} length should be greater or equal to 5.")
                .MaximumLength(20).WithMessage("{PropertyName} length should be less or equal to 20.");

            RuleFor(a => a.AddressLine)
                .NotNull().WithMessage("{PropertyName} should not be null.")
                .MinimumLength(7).WithMessage("{PropertyName} length should be greater or equal to 7.")
                .MaximumLength(45).WithMessage("{PropertyName} length should be less or equal to 45.");

            RuleFor(a => a.SuiteNumber)
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0.");

            RuleFor(a => a.PostalCode)
                .NotNull().WithMessage("{PropertyName} should not be null.")
                .MinimumLength(7).WithMessage("{PropertyName} length should be greater or equal to 7.")
                .MaximumLength(25).WithMessage("{PropertyName} length should be less or equal to 25.");
        }
    }
}
