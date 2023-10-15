using AutoMapper;
using FluentValidation;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using MediatR;
using ValidationException = GloboDelivery.Application.Exceptions.ValidationException;

namespace GloboDelivery.Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, AddressDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateAddressCommand> _validator;

        public CreateAddressCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IValidator<CreateAddressCommand> validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<AddressDto> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var addressToAdd = _mapper.Map<Address>(request);

            await _unitOfWork.Repository<Address>().CreateAsync(addressToAdd);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AddressDto>(addressToAdd);
        }
    }
}
