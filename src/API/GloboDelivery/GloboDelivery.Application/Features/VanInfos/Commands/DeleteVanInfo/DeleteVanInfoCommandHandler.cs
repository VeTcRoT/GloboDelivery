using GloboDelivery.Application.Exceptions;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using MediatR;

namespace GloboDelivery.Application.Features.VanInfos.Commands.DeleteVanInfo
{
    public class DeleteVanInfoCommandHandler : IRequestHandler<DeleteVanInfoCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVanInfoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteVanInfoCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<VanInfo>();

            var vanInfoToDelete = await repository.GetByIdAsync(request.Id);

            if (vanInfoToDelete == null)
                throw new NotFoundException(nameof(VanInfo), request.Id);

            repository.Delete(vanInfoToDelete);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
