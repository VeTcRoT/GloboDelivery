using AutoMapper;
using GloboDelivery.Application.Exceptions;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using MediatR;

namespace GloboDelivery.Application.Features.VanInfos.Queries.GetVanInfoById
{
    public class GetVanInfoByIdCommandHandler : IRequestHandler<GetVanInfoByIdCommand, VanInfoDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetVanInfoByIdCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<VanInfoDto> Handle(GetVanInfoByIdCommand request, CancellationToken cancellationToken)
        {
            var vanInfo = await _unitOfWork.Repository<VanInfo>().GetByIdAsync(request.Id);

            if (vanInfo == null)
                throw new NotFoundException(nameof(VanInfo), request.Id);

            return _mapper.Map<VanInfoDto>(vanInfo);
        }
    }
}
