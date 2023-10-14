using AutoMapper;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Interfaces;
using MediatR;

namespace GloboDelivery.Application.Features.Deliveries.Queries.GetAllDeliveries
{
    public class GetAllDeliveriesQueryHandler : IRequestHandler<GetAllDeliveriesQuery, IReadOnlyList<DeliveryListingDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllDeliveriesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<DeliveryListingDto>> Handle(GetAllDeliveriesQuery request, CancellationToken cancellationToken)
        {
            var deliveries = await _unitOfWork.DeliveryRepository.ListAllAsync();

            return _mapper.Map<IReadOnlyList<DeliveryListingDto>>(deliveries);
        }
    }
}
