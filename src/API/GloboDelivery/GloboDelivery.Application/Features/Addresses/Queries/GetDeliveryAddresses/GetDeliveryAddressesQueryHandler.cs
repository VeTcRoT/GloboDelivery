using AutoMapper;
using GloboDelivery.Application.Exceptions;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using MediatR;

namespace GloboDelivery.Application.Features.Addresses.Queries.GetDeliveryAddresses
{
    public class GetDeliveryAddressesQueryHandler : IRequestHandler<GetDeliveryAddressesQuery, IReadOnlyList<AddressDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetDeliveryAddressesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<AddressDto>> Handle(GetDeliveryAddressesQuery request, CancellationToken cancellationToken)
        {
            var deliveryAddresses = await _unitOfWork.DeliveryRepository.GetDeliveryAddressesAsync(request.DeliveryId);

            if (deliveryAddresses == null)
                throw new NotFoundException(nameof(Delivery), request.DeliveryId);

            return _mapper.Map<IReadOnlyList<AddressDto>>(deliveryAddresses);
        }
    }
}
