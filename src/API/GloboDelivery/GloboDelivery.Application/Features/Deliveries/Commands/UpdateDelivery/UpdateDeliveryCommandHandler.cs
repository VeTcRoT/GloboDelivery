using AutoMapper;
using FluentValidation;
using GloboDelivery.Application.Exceptions;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using MediatR;
using ValidationException = GloboDelivery.Application.Exceptions.ValidationException;

namespace GloboDelivery.Application.Features.Deliveries.Commands.UpdateDelivery
{
    public class UpdateDeliveryCommandHandler : IRequestHandler<UpdateDeliveryCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateDeliveryCommand> _validator;

        public UpdateDeliveryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IValidator<UpdateDeliveryCommand> validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task Handle(UpdateDeliveryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var deliveryToUpdate = await _unitOfWork.DeliveryRepository.GetByIdWithDeliveryAddressesAsync(request.Id);

            if (deliveryToUpdate == null)
                throw new NotFoundException(nameof(Delivery), request.Id);

            if (deliveryToUpdate.VanInfoId != request.VanInfoId)
            {
                var vanInfo = await _unitOfWork.Repository<VanInfo>().GetByIdAsync(request.VanInfoId);

                if (vanInfo == null)
                    throw new NotFoundException(nameof(VanInfo), request.VanInfoId);

                deliveryToUpdate.VanInfo = vanInfo;
            }

            var addresses = await _unitOfWork.AddressRepository.GetAddressesByIdsAsync(request.AddressesIds);

            var notFoundIds = request.AddressesIds.Except(addresses.Select(a => a.Id)).ToList();

            if (notFoundIds.Any())
                throw new NotFoundException(nameof(Address), string.Join(", ", notFoundIds));

            var deliveryAddressRepo = _unitOfWork.Repository<DeliveryAddress>();

            DeleteDeliveryAddresses(deliveryToUpdate, deliveryAddressRepo);
            await CreateDeliveryAddressesAsync(deliveryToUpdate, addresses, deliveryAddressRepo);

            await _unitOfWork.SaveChangesAsync();
        }

        private void DeleteDeliveryAddresses(Delivery deliveryToUpdate, IBaseRepository<DeliveryAddress> deliveryAddressRepo)
        {
            foreach (var deliveryAddress in deliveryToUpdate.DeliveryAddresses)
                deliveryAddressRepo.Delete(deliveryAddress);
        }

        private async Task CreateDeliveryAddressesAsync(Delivery deliveryToUpdate, IEnumerable<Address> addresses, IBaseRepository<DeliveryAddress> deliveryAddressRepo)
        {
            foreach (var address in addresses)
            {
                await deliveryAddressRepo.CreateAsync(new DeliveryAddress
                {
                    Delivery = deliveryToUpdate,
                    Address = address
                });
            }
        }

    }
}
