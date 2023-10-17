using AutoMapper;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Helpers;
using GloboDelivery.Domain.Interfaces;
using MediatR;

namespace GloboDelivery.Application.Features.Addresses.Queries.GetAllAddresses
{
    public class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQuery, PagedList<AddressDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAddressesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<AddressDto>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
        {
            var addresses = await _unitOfWork.AddressRepository.ListPagedAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PagedList<AddressDto>>(addresses);
        }
    }
}
