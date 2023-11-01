using AutoMapper;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Helpers;
using GloboDelivery.Domain.Interfaces;
using MediatR;

namespace GloboDelivery.Application.Features.Deliveries.Queries.GetDeliveriesByCondition
{
    public class GetDeliveriesByConditionQueryHandler : IRequestHandler<GetDeliveriesByConditionQuery, PagedList<FullDeliveryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDeliveriesByConditionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedList<FullDeliveryDto>> Handle(GetDeliveriesByConditionQuery request, CancellationToken cancellationToken)
        {
            var deliveries = await _unitOfWork.DeliveryRepository
                .GetPagedDeliveryAddressesByConditionAsync(
                request.PageNumber, request.PageSize, request.MinCapacity, request.DepartureDate, request.ArrivalDate.Date, 
                request.DepartureCountry, request.DepartureCity, request.ArrivalCountry, request.ArrivalCity);

            return _mapper.Map<PagedList<FullDeliveryDto>>(deliveries);
        }
    }
}
