using AutoMapper;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using MediatR;

namespace GloboDelivery.Application.Features.VanInfos.Queries.GetAllVanInfos
{
    public class GetAllVanInfosQueryHandler : IRequestHandler<GetAllVanInfosQuery, IReadOnlyList<VanInfoDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllVanInfosQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<VanInfoDto>> Handle(GetAllVanInfosQuery request, CancellationToken cancellationToken)
        {
            var vanInfos = await _unitOfWork.Repository<VanInfo>().ListAllAsync();

            return _mapper.Map<IReadOnlyList<VanInfoDto>>(vanInfos);
        }
    }
}
