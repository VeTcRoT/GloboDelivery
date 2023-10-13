using AutoMapper;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using MediatR;

namespace GloboDelivery.Application.Features.VanInfos.Queries.GetAllVanInfos
{
    public class GetAllVanInfosQueryHandler : IRequestHandler<GetAllVanInfosQuery, List<VanInfoDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllVanInfosQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<VanInfoDto>> Handle(GetAllVanInfosQuery request, CancellationToken cancellationToken)
        {
            var vanInfos = await _unitOfWork.Repository<VanInfo>().ListAllAsync();

            return _mapper.Map<List<VanInfoDto>>(vanInfos);
        }
    }
}
