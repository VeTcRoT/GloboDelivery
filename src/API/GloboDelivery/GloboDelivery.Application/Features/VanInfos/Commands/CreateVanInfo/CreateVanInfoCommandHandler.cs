using AutoMapper;
using FluentValidation;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using MediatR;
using ValidationException = GloboDelivery.Application.Exceptions.ValidationException;

namespace GloboDelivery.Application.Features.VanInfos.Commands.CreateVanInfo
{
    public class CreateVanInfoCommandHandler : IRequestHandler<CreateVanInfoCommand, VanInfoDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateVanInfoCommand> _validator;

        public CreateVanInfoCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IValidator<CreateVanInfoCommand> validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<VanInfoDto> Handle(CreateVanInfoCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult);

            var vanInfoToAdd = _mapper.Map<VanInfo>(request);

            await _unitOfWork.Repository<VanInfo>().CreateAsync(vanInfoToAdd);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<VanInfoDto>(vanInfoToAdd);
        }
    }
}
