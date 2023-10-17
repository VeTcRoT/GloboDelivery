using AutoMapper;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Helpers;
using GloboDelivery.Domain.Interfaces;
using MediatR;

namespace GloboDelivery.Application.Features.Deliveries.Queries.GetAllDeliveries
{
    public class GetAllDeliveriesQueryHandler : IRequestHandler<GetAllDeliveriesQuery, PagedList<DeliveryDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllDeliveriesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<DeliveryDto>> Handle(GetAllDeliveriesQuery request, CancellationToken cancellationToken)
        {
            var deliveries = await _unitOfWork.DeliveryRepository.ListPagedAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PagedList<DeliveryDto>>(deliveries);
        }
    }
}
