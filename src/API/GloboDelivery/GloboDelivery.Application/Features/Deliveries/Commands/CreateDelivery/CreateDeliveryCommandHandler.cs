using AutoMapper;
using FluentValidation;
using GloboDelivery.Application.Exceptions;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using MediatR;
using ValidationException = GloboDelivery.Application.Exceptions.ValidationException;

namespace GloboDelivery.Application.Features.Deliveries.Commands.CreateDelivery
{
    public class CreateDeliveryCommandhandler : IRequestHandler<CreateDeliveryCommand, CreateDeliveryDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateDeliveryCommand> _validator;

        public CreateDeliveryCommandhandler(IMapper mapper, IUnitOfWork unitOfWork, IValidator<CreateDeliveryCommand> validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<CreateDeliveryDto> Handle(CreateDeliveryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var addresses = await _unitOfWork.AddressRepository.GetAddressesByIdsAsync(request.AddressesIds);

            if (addresses == null)
                throw new NotFoundException(nameof(Address), string.Join(", ", request.AddressesIds));

            var notFoundIds = request.AddressesIds
                .Where(id => !addresses.Where(a => a.Id == id).Any())
                .Select(id => id).ToList();

            if (notFoundIds.Any())
                throw new NotFoundException(nameof(Address), string.Join(", ", notFoundIds));

            var deliveryToAdd = _mapper.Map<Delivery>(request);

#pragma warning disable CS8601 // Possible null reference assignment.
            deliveryToAdd.VanInfo = await _unitOfWork.Repository<VanInfo>().GetByIdAsync(request.VanInfoId);
#pragma warning restore CS8601 // Possible null reference assignment.

            await _unitOfWork.Repository<Delivery>().CreateAsync(deliveryToAdd);

            var deliveryAddressRepo = _unitOfWork.Repository<DeliveryAddress>();

            foreach (var address in addresses)
            {
                var deliveryAddress = new DeliveryAddress
                {
                    Delivery = deliveryToAdd,
                    Address = address
                };

                await deliveryAddressRepo.CreateAsync(deliveryAddress);
            }

            await _unitOfWork.SaveChangesAsync();

            var deliveryToReturn = _mapper.Map<CreateDeliveryDto>(deliveryToAdd);
            deliveryToReturn.Addresses = _mapper.Map<IReadOnlyList<AddressDto>>(addresses);

            return deliveryToReturn;
        }
    }
}
