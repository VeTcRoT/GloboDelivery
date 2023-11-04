using AutoMapper;
using FluentValidation;
using GloboDelivery.Application.Services.Infrastructure;
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
        private readonly IAddressValidationService _addressValidationService;

        public CreateAddressCommandHandler(
            IMapper mapper, 
            IUnitOfWork unitOfWork, 
            IValidator<CreateAddressCommand> validator, 
            IAddressValidationService addressValidationService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _addressValidationService = addressValidationService;
        }

        public async Task<AddressDto> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var addressValidationResult = _addressValidationService.ValidateAddress(request);

            var addressToAdd = _mapper.Map<Address>(addressValidationResult);

            addressToAdd.Country = request.Country;

            await _unitOfWork.Repository<Address>().CreateAsync(addressToAdd);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AddressDto>(addressToAdd);
        }
    }
}
