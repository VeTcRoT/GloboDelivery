using GloboDelivery.Application.Exceptions;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using MediatR;

namespace GloboDelivery.Application.Features.Addresses.DeleteAddress
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAddressCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<Address>();

            var addressToDelete = await repository.GetByIdAsync(request.Id);

            if (addressToDelete == null)
                throw new NotFoundException(nameof(Address), request.Id);

            repository.Delete(addressToDelete);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
