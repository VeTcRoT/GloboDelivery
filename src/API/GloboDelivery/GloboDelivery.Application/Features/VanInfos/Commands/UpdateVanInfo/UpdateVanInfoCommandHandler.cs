using AutoMapper;
using FluentValidation;
using GloboDelivery.Application.Exceptions;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using MediatR;
using ValidationException = GloboDelivery.Application.Exceptions.ValidationException;

namespace GloboDelivery.Application.Features.VanInfos.Commands.UpdateVanInfo
{
    public class UpdateVanInfoCommandHandler : IRequestHandler<UpdateVanInfoCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpdateVanInfoCommand> _validator;

        public UpdateVanInfoCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IValidator<UpdateVanInfoCommand> validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task Handle(UpdateVanInfoCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var vanInfoFromRepo = await _unitOfWork.Repository<VanInfo>().GetByIdAsync(request.Id);

            if (vanInfoFromRepo == null)
                throw new NotFoundException(nameof(VanInfo), request.Id);

            _mapper.Map(request, vanInfoFromRepo);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
