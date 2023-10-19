using AutoFixture;
using FluentAssertions;
using GloboDelivery.Application.Exceptions;
using GloboDelivery.Application.Features.VanInfos.Commands.DeleteVanInfo;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using Moq;

namespace Application.UnitTests.Features.VanInfos.Commands
{
    public class DeleteVanInfoCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IBaseRepository<VanInfo>> _repositoryMock;
        private readonly Fixture _fixture;
        private readonly DeleteVanInfoCommand _deleteVanInfoCommand;

        public DeleteVanInfoCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IBaseRepository<VanInfo>>();
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _deleteVanInfoCommand = new DeleteVanInfoCommand { Id = 1 };
        }

        [Fact]
        public async Task Handle_Invoke_AllGood()
        {
            //Arrange
            var vanInfoToDelete = _fixture.Build<VanInfo>().Create();
            _repositoryMock.Setup(r => r.GetByIdAsync(_deleteVanInfoCommand.Id)).ReturnsAsync(vanInfoToDelete);
            _repositoryMock.Setup(r => r.Delete(vanInfoToDelete));

            _unitOfWorkMock.Setup(u => u.Repository<VanInfo>()).Returns(_repositoryMock.Object);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync());

            //Act
            await new DeleteVanInfoCommandHandler(_unitOfWorkMock.Object).Handle(_deleteVanInfoCommand, CancellationToken.None);

            //Assert
            _repositoryMock.Verify(r => r.GetByIdAsync(_deleteVanInfoCommand.Id), Times.Once);
            _repositoryMock.Verify(r => r.Delete(vanInfoToDelete), Times.Once);
            _unitOfWorkMock.Verify(u => u.Repository<VanInfo>(), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_InvokeWithNotFoundVanInfo()
        {
            //Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync(_deleteVanInfoCommand.Id)).ReturnsAsync(null as VanInfo);

            _unitOfWorkMock.Setup(u => u.Repository<VanInfo>()).Returns(_repositoryMock.Object);

            //Act and Assert
            Func<Task> act = async() => await new DeleteVanInfoCommandHandler(_unitOfWorkMock.Object).Handle(_deleteVanInfoCommand, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>().WithMessage($"{nameof(VanInfo)} ({_deleteVanInfoCommand.Id}) is not found");

            _repositoryMock.Verify(r => r.GetByIdAsync(_deleteVanInfoCommand.Id), Times.Once);
            _unitOfWorkMock.Verify(u => u.Repository<VanInfo>(), Times.Once);
        }
    }
}
