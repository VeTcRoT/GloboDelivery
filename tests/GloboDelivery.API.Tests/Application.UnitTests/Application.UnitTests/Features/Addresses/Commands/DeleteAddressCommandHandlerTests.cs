using AutoFixture;
using FluentAssertions;
using GloboDelivery.Application.Exceptions;
using GloboDelivery.Application.Features.Addresses.Commands.DeleteAddress;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using Moq;

namespace Application.UnitTests.Features.Addresses.Commands
{
    public class DeleteAddressCommandHandlerTests
    {
        private readonly DeleteAddressCommand _deleteAddressCommand;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IBaseRepository<Address>> _baseAddressMockRepository;
        private readonly Fixture _fixture;

        public DeleteAddressCommandHandlerTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _deleteAddressCommand = _fixture.Build<DeleteAddressCommand>().Create();

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _baseAddressMockRepository = new Mock<IBaseRepository<Address>>();
        }

        [Fact]
        public async Task Handle_Invoke_AllGood()
        {
            //Arrange
            var addressToDelete = _fixture.Build<Address>().Create();

            _baseAddressMockRepository.Setup(r => r.GetByIdAsync(_deleteAddressCommand.Id)).ReturnsAsync(addressToDelete);
            _baseAddressMockRepository.Setup(r => r.Delete(addressToDelete));

            _unitOfWorkMock.Setup(u => u.Repository<Address>()).Returns(_baseAddressMockRepository.Object);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync());

            //Act
            await new DeleteAddressCommandHandler(_unitOfWorkMock.Object).Handle(_deleteAddressCommand, CancellationToken.None);

            //Assert
            _baseAddressMockRepository.Verify(r => r.GetByIdAsync(_deleteAddressCommand.Id), Times.Once);
            _baseAddressMockRepository.Verify(r => r.Delete(addressToDelete), Times.Once);
            _unitOfWorkMock.Verify(u => u.Repository<Address>(), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_InvokeWithNotFoundAddress_NotFoundExceptionOccures()
        {
            //Arrange
            _baseAddressMockRepository.Setup(r => r.GetByIdAsync(_deleteAddressCommand.Id)).ReturnsAsync(null as Address);

            _unitOfWorkMock.Setup(u => u.Repository<Address>()).Returns(_baseAddressMockRepository.Object);

            //Act and Assert
            Func<Task> act = async () =>
                await new DeleteAddressCommandHandler(_unitOfWorkMock.Object)
                .Handle(_deleteAddressCommand, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>();

            _baseAddressMockRepository.Verify(r => r.GetByIdAsync(_deleteAddressCommand.Id), Times.Once);
            _unitOfWorkMock.Verify(u => u.Repository<Address>(), Times.Once);
        }
    }
}
