using AutoMapper;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Helpers;
using GloboDelivery.Domain.Interfaces;
using MediatR;

namespace GloboDelivery.Application.Features.VanInfos.Queries.GetAllVanInfos
{
    public class GetAllVanInfosQueryHandler : IRequestHandler<GetAllVanInfosQuery, PagedList<VanInfoDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllVanInfosQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<VanInfoDto>> Handle(GetAllVanInfosQuery request, CancellationToken cancellationToken)
        {
            var vanInfos = await _unitOfWork.Repository<VanInfo>().ListPagedAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PagedList<VanInfoDto>>(vanInfos);
        }
    }
}
