using GloboDelivery.Application.Exceptions;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using MediatR;

namespace GloboDelivery.Application.Features.Deliveries.Commands.DeleteDelivery
{
    public class DeleteDeliveryCommandHandler : IRequestHandler<DeleteDeliveryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDeliveryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteDeliveryCommand request, CancellationToken cancellationToken)
        {
            var deliveryToDelete = await _unitOfWork.DeliveryRepository.GetByIdAsync(request.Id);

            if (deliveryToDelete == null)
                throw new NotFoundException(nameof(Delivery), request.Id);

            _unitOfWork.DeliveryRepository.Delete(deliveryToDelete);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
