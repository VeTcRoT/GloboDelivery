using AutoFixture;
using AutoMapper;
using Bogus;
using FluentAssertions;
using FluentValidation;
using GloboDelivery.Application.Features.VanInfos.Commands.CreateVanInfo;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using Moq;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Application.UnitTests.Features.VanInfos.Commands
{
    public class CreateVanInfoCommandHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidator<CreateVanInfoCommand>> _validatorMock;
        private readonly Fixture _fixture;
        private readonly CreateVanInfoCommand _createVanInfoCommand;

        public CreateVanInfoCommandHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validatorMock = new Mock<IValidator<CreateVanInfoCommand>>();
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var createVanInfoCommandFaker = new Faker<CreateVanInfoCommand>()
                .RuleFor(c => c.Mark, f => f.Random.Word())
                .RuleFor(c => c.Model, f => f.Lorem.Word())
                .RuleFor(c => c.Year, f => f.Random.Number(2000, 2023))
                .RuleFor(c => c.Color, f => f.Random.Word())
                .RuleFor(c => c.Capacity, f => f.Random.Decimal(5000, 35000))
                .RuleFor(c => c.LastInspectionDate, f => f.Date.Future(1))
                .RuleFor(c => c.Remarks, f => f.Lorem.Text());
            _createVanInfoCommand = createVanInfoCommandFaker.Generate();
        }

        [Fact]
        public async Task Hande_Invoke_AllGood()
        {
            //Arrange
            var validationResult = new Mock<ValidationResult>();
            validationResult.Setup(vr => vr.IsValid).Returns(true);

            _validatorMock.Setup(v => v.ValidateAsync(_createVanInfoCommand, CancellationToken.None)).ReturnsAsync(validationResult.Object);

            var vanInfoToAdd = _fixture.Build<VanInfo>().Create();

            _mapperMock.Setup(m => m.Map<VanInfo>(_createVanInfoCommand)).Returns(vanInfoToAdd);

            var repository = new Mock<IBaseRepository<VanInfo>>();
            repository.Setup(r => r.CreateAsync(vanInfoToAdd));

            _unitOfWorkMock.Setup(u => u.Repository<VanInfo>()).Returns(repository.Object);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync());

            var vanInfoToReturn = _fixture.Build<VanInfoDto>().Create();
            _mapperMock.Setup(m => m.Map<VanInfoDto>(vanInfoToAdd)).Returns(vanInfoToReturn);

            //Act
            var result = await new CreateVanInfoCommandHandler(_mapperMock.Object, _unitOfWorkMock.Object, _validatorMock.Object)
                .Handle(_createVanInfoCommand, CancellationToken.None);

            //Assert
            _validatorMock.Verify(v => v.ValidateAsync(_createVanInfoCommand, CancellationToken.None), Times.Once);
            _mapperMock.Verify(m => m.Map<VanInfo>(_createVanInfoCommand), Times.Once);
            repository.Verify(r => r.CreateAsync(vanInfoToAdd), Times.Once);
            _unitOfWorkMock.Verify(u => u.Repository<VanInfo>(), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<VanInfoDto>(vanInfoToAdd), Times.Once);

            result.Should().BeEquivalentTo(vanInfoToReturn);
        }

        [Fact]
        public async Task Handle_InvokeWithValidationError_ValidationExceptionOccures()
        {
            //Arrange
            var validationResult = new Mock<ValidationResult>();
            validationResult.Setup(vr => vr.IsValid).Returns(false);

            _validatorMock.Setup(v => v.ValidateAsync(_createVanInfoCommand, CancellationToken.None)).ReturnsAsync(validationResult.Object);

            //Act and Assert
            Func<Task> act = async() => await new CreateVanInfoCommandHandler(_mapperMock.Object, _unitOfWorkMock.Object, _validatorMock.Object)
                .Handle(_createVanInfoCommand, CancellationToken.None);

            await act.Should().ThrowAsync<GloboDelivery.Application.Exceptions.ValidationException>();

            _validatorMock.Verify(v => v.ValidateAsync(_createVanInfoCommand, CancellationToken.None), Times.Once);
        }
    }
}
