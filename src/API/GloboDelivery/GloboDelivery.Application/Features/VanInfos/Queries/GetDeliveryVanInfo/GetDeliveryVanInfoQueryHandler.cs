using AutoMapper;
using GloboDelivery.Application.Exceptions;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using MediatR;

namespace GloboDelivery.Application.Features.VanInfos.Queries.GetDeliveryVanInfo
{
    public class GetDeliveryVanInfoQueryHandler : IRequestHandler<GetDeliveryVanInfoQuery, VanInfoDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetDeliveryVanInfoQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<VanInfoDto> Handle(GetDeliveryVanInfoQuery request, CancellationToken cancellationToken)
        {
            var vanInfo = await _unitOfWork.DeliveryRepository.GetDeliveryVanInfoAsync(request.DeliveryId);

            if (vanInfo == null)
                throw new NotFoundException(nameof(Delivery), request.DeliveryId);

            return _mapper.Map<VanInfoDto>(vanInfo);
        }
    }
}
