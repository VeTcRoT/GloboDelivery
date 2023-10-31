using FluentValidation;
using GloboDelivery.Application.Exceptions;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;

namespace GloboDelivery.Application.Features.Deliveries.Commands.CreateDelivery
{
    public class CreateDeliveryCommandValidator : AbstractValidator<CreateDeliveryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateDeliveryCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(d => d.Price)
                .GreaterThan(20).WithMessage("{PropertyName} should be greater than 20.");

            RuleFor(d => d.CapacityTaken)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} should be greater of equal to 0.");

            RuleFor(d => d.AddressesDates)
                .Must(ai => ai.Count() <= 5).WithMessage("{PropertyName} should not exceed a maximum length of 5 elements.")
                .Must(DepartureDatesShouldBeGreaterThanCurrentDate).WithMessage("Departure date should be greater or equal to current datetime.")
                .Must(ArrivalDatesShouldBeGreaterThanArrivalDates).WithMessage("Arrival date should be greater than departure date.");

            RuleFor(d => d)
                .MustAsync(CapacityTakenLessOrEqualToVanCapacity).WithMessage("CapacityTaken should be less or equal to Capacity of Van.");
        }

        private bool DepartureDatesShouldBeGreaterThanCurrentDate(IEnumerable<AddressDate> addressDates)
        {
            foreach (var addressDate in addressDates)
            {
                if (addressDate.DepartureDate < DateTime.Now)
                    return false;
            }

            return true;
        }

        private bool ArrivalDatesShouldBeGreaterThanArrivalDates(IEnumerable<AddressDate> addressDates)
        {
            foreach (var addressDate in addressDates)
            {
                if (addressDate.DepartureDate >= addressDate.ArrivalDate)
                    return false;
            }

            return true;
        }

        private async Task<bool> CapacityTakenLessOrEqualToVanCapacity(CreateDeliveryCommand command, CancellationToken cancellationToken)
        {
            var van = await _unitOfWork.Repository<VanInfo>().GetByIdAsync(command.VanInfoId);

            if (van == null)
                throw new NotFoundException(nameof(VanInfo), command.VanInfoId);

            return van.Capacity >= command.CapacityTaken;
        }
    }
}
