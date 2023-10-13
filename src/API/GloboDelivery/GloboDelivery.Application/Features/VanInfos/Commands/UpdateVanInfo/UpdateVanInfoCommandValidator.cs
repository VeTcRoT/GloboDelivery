using FluentValidation;

namespace GloboDelivery.Application.Features.VanInfos.Commands.UpdateVanInfo
{
    public class UpdateVanInfoCommandValidator : AbstractValidator<UpdateVanInfoCommand>
    {
        public UpdateVanInfoCommandValidator()
        {
            RuleFor(v => v.Mark)
                .NotNull().WithMessage("Value for {PropertyName} should be provided.")
                .MinimumLength(3).WithMessage("Minimum length for {PropertyName} is 3 characters.")
                .MaximumLength(20).WithMessage("Maximum length for {PropertyName} is 20 characters.");

            RuleFor(v => v.Model)
                .NotNull().WithMessage("Value for {PropertyName} should be provided.")
                .MinimumLength(3).WithMessage("Minimum length for {PropertyName} is 3 characters.")
                .MaximumLength(20).WithMessage("Maximum length for {PropertyName} is 20 characters.");

            RuleFor(v => v.Year)
                .GreaterThanOrEqualTo(2000).WithMessage("{PropertyName} of van should be greater than 2000.")
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} of van should be less or equal to " + DateTime.Now.Year);

            RuleFor(v => v.Color)
                .NotNull().WithMessage("Value for {PropertyName} should be provided.")
                .MinimumLength(3).WithMessage("Minimum length for {PropertyName} is 3 characters.")
                .MaximumLength(15).WithMessage("Maximum length for {PropertyName} is 15 characters.");

            RuleFor(v => v.Capacity)
                .GreaterThanOrEqualTo(3000).WithMessage("{PropertyName} of van should be greater than 3000 kg.")
                .LessThanOrEqualTo(40000).WithMessage("{PropertyName} of van should be less or equal to 40000 kg.");

            RuleFor(v => v.LastInspectionDate)
                .Must(InspectionDateLessOrEqualToCurrentYear).WithMessage("{PropertyName} should not be less than a year ago.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("{PropertyName} should not be more than current date.");

            RuleFor(v => v.Remarks)
                .MaximumLength(250).WithMessage("Length of {PropertyName} should not be more than 250 characters.");
        }

        private bool InspectionDateLessOrEqualToCurrentYear(DateTime lastInspectionDate)
        {
            var offset = DateTime.Now - lastInspectionDate;

            return offset.Days <= 365;
        }
    }
}
