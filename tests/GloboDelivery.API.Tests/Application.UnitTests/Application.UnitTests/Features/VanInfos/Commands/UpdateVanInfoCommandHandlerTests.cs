using AutoFixture;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using GloboDelivery.Application.Features.VanInfos.Commands.UpdateVanInfo;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using Moq;

namespace Application.UnitTests.Features.VanInfos.Commands
{
    public class UpdateVanInfoCommandHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidator<UpdateVanInfoCommand>> _validatorMock;
        private readonly Mock<ValidationResult> _validationResultMock;
        private readonly Fixture _fixture;
        private readonly UpdateVanInfoCommand _updateVanInfoCommand;

        public UpdateVanInfoCommandHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validatorMock = new Mock<IValidator<UpdateVanInfoCommand>>();
            _validationResultMock = new Mock<ValidationResult>();
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _updateVanInfoCommand = _fixture.Build<UpdateVanInfoCommand>().Create();
        }

        [Fact]
        public async Task Handle_Invoke_AllGood()
        {
            //Arrange
            _validationResultMock.Setup(v => v.IsValid).Returns(true);
            _validatorMock.Setup(v => v.ValidateAsync(_updateVanInfoCommand, CancellationToken.None)).ReturnsAsync(_validationResultMock.Object);

            var repository = new Mock<IBaseRepository<VanInfo>>();
            var vanInfoFromRepo = _fixture.Build<VanInfo>().Create();
            repository.Setup(r => r.GetByIdAsync(_updateVanInfoCommand.Id)).ReturnsAsync(vanInfoFromRepo);
            _unitOfWorkMock.Setup(u => u.Repository<VanInfo>()).Returns(repository.Object);

            _mapperMock.Setup(m => m.Map(_updateVanInfoCommand, vanInfoFromRepo));

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync());

            //Act
            await new UpdateVanInfoCommandHandler(_mapperMock.Object, _unitOfWorkMock.Object, _validatorMock.Object)
                .Handle(_updateVanInfoCommand, CancellationToken.None);

            //Assert
            _validationResultMock.Verify(v => v.IsValid, Times.Once);
            _validatorMock.Verify(v => v.ValidateAsync(_updateVanInfoCommand, CancellationToken.None), Times.Once);
            repository.Verify(r => r.GetByIdAsync(_updateVanInfoCommand.Id), Times.Once);
            _unitOfWorkMock.Verify(u => u.Repository<VanInfo>(), Times.Once);
            _mapperMock.Verify(m => m.Map(_updateVanInfoCommand, vanInfoFromRepo), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_InvokeWithValidationError_ValidationExceptionOccures()
        {
            //Arrange
            _validationResultMock.Setup(v => v.IsValid).Returns(false);
            _validatorMock.Setup(v => v.ValidateAsync(_updateVanInfoCommand, CancellationToken.None)).ReturnsAsync(_validationResultMock.Object);

            //Act and Assert
            Func<Task> act = async() => await new UpdateVanInfoCommandHandler(_mapperMock.Object, _unitOfWorkMock.Object, _validatorMock.Object)
                .Handle(_updateVanInfoCommand, CancellationToken.None);

            await act.Should().ThrowAsync<GloboDelivery.Application.Exceptions.ValidationException>();

            _validationResultMock.Verify(v => v.IsValid, Times.Once);
            _validatorMock.Verify(v => v.ValidateAsync(_updateVanInfoCommand, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Handle_InvokeWithNotFoundVanInfo_NotFoundExceptionOccures()
        {
            //Arrange
            _validationResultMock.Setup(v => v.IsValid).Returns(true);
            _validatorMock.Setup(v => v.ValidateAsync(_updateVanInfoCommand, CancellationToken.None)).ReturnsAsync(_validationResultMock.Object);

            var repository = new Mock<IBaseRepository<VanInfo>>();
            repository.Setup(r => r.GetByIdAsync(_updateVanInfoCommand.Id)).ReturnsAsync(null as VanInfo);
            _unitOfWorkMock.Setup(u => u.Repository<VanInfo>()).Returns(repository.Object);

            //Act and Assert
            Func<Task> act = async () => await new UpdateVanInfoCommandHandler(_mapperMock.Object, _unitOfWorkMock.Object, _validatorMock.Object)
                .Handle(_updateVanInfoCommand, CancellationToken.None);

            await act.Should().ThrowAsync<GloboDelivery.Application.Exceptions.NotFoundException>().WithMessage($"{nameof(VanInfo)} ({_updateVanInfoCommand.Id}) is not found");

            _validationResultMock.Verify(v => v.IsValid, Times.Once);
            _validatorMock.Verify(v => v.ValidateAsync(_updateVanInfoCommand, CancellationToken.None), Times.Once);
            repository.Verify(r => r.GetByIdAsync(_updateVanInfoCommand.Id), Times.Once);
            _unitOfWorkMock.Verify(u => u.Repository<VanInfo>(),Times.Once);
        }
    }
}
