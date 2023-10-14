using FluentValidation;
using GloboDelivery.Application.Exceptions;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;

namespace GloboDelivery.Application.Features.Deliveries.Commands.UpdateDelivery
{
    public class UpdateDeliveryCommandValidator : AbstractValidator<UpdateDeliveryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateDeliveryCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(d => d.Price)
                .GreaterThan(20).WithMessage("{PropertyName} should be greater than 20.");

            RuleFor(d => d.CapacityTaken)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} should be greater of equal to 0.");

            RuleFor(d => d.DepartureDate)
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("{PropertyName} should be greater or equal to " + DateTime.Now);

            RuleFor(d => d.AddressesIds)
                .Must(ai => ai.Count() <= 5).WithMessage("{PropertyName} should not exceed a maximum length of 5 elements.");

            RuleFor(d => d)
                .Must(ArrivalDateGreaterThanDepartureDate).WithMessage("ArrivalDate should be greater than DepartureDate.")
                .MustAsync(CapacityTakenLessOrEqualToVanCapacity).WithMessage("CapacityTaken should be less or equal to Capacity of Van.");
        }

        private bool ArrivalDateGreaterThanDepartureDate(UpdateDeliveryCommand command)
        {
            return command.ArrivalDate > command.DepartureDate;
        }

        private async Task<bool> CapacityTakenLessOrEqualToVanCapacity(UpdateDeliveryCommand command, CancellationToken cancellationToken)
        {
            var van = await _unitOfWork.Repository<VanInfo>().GetByIdAsync(command.VanInfoId);

            if (van == null)
                throw new NotFoundException(nameof(VanInfo), command.VanInfoId);

            return van.Capacity >= command.CapacityTaken;
        }
    }
}
