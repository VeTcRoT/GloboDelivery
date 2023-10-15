using AutoMapper;
using GloboDelivery.Application.Exceptions;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using MediatR;

namespace GloboDelivery.Application.Features.Deliveries.Queries.GetDeliveryById
{
    public class GetDeliveryByIdQueryHandler : IRequestHandler<GetDeliveryByIdQuery, DeliveryDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetDeliveryByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeliveryDto> Handle(GetDeliveryByIdQuery request, CancellationToken cancellationToken)
        {
            var delivery = await _unitOfWork.DeliveryRepository.GetByIdAsync(request.Id);

            if (delivery == null)
                throw new NotFoundException(nameof(Delivery), request.Id);

            return _mapper.Map<DeliveryDto>(delivery);
        }
    }
}
