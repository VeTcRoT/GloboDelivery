using AutoMapper;
using FluentValidation;
using GloboDelivery.Application.Exceptions;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using MediatR;
using ValidationException = GloboDelivery.Application.Exceptions.ValidationException;

namespace GloboDelivery.Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateAddressCommand> _validator;

        public UpdateAddressCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IValidator<UpdateAddressCommand> validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var addressToUpdate = await _unitOfWork.Repository<Address>().GetByIdAsync(request.Id);

            if (addressToUpdate == null)
                throw new NotFoundException(nameof(Address), request.Id);

            _mapper.Map(request, addressToUpdate);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
