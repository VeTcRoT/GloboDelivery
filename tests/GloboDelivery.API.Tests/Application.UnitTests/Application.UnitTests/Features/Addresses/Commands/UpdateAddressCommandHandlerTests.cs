using AutoFixture;
using AutoMapper;
using Bogus;
using FluentAssertions;
using FluentValidation;
using GloboDelivery.Application.Features.Addresses.Commands.UpdateAddress;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using Moq;
using ValidationException = GloboDelivery.Application.Exceptions.ValidationException;

namespace Application.UnitTests.Features.Addresses.Commands
{
    public class UpdateAddressCommandHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidator<UpdateAddressCommand>> _updateAddressCommandValidatorMock;
        private readonly UpdateAddressCommand _updateAddressCommand;
        private readonly Mock<FluentValidation.Results.ValidationResult> _validationResultMock;
        public UpdateAddressCommandHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _updateAddressCommandValidatorMock = new Mock<IValidator<UpdateAddressCommand>>();

            var updateAddressCommndFaker = new Faker<UpdateAddressCommand>()
                .RuleFor(c => c.Id, f => f.Random.Number(1, 10))
                .RuleFor(c => c.Country, f => f.Address.County())
                .RuleFor(c => c.City, f => f.Address.City())
                .RuleFor(c => c.AdministrativeArea, f => f.Address.State())
                .RuleFor(c => c.AddressLine, f => f.Address.StreetAddress())
                .RuleFor(c => c.SuiteNumber, f => f.Random.Number(1, 10))
                .RuleFor(c => c.PostalCode, f => f.Address.ZipCode());

            _updateAddressCommand = updateAddressCommndFaker.Generate();
            _validationResultMock = new Mock<FluentValidation.Results.ValidationResult>();
        }

        [Fact]
        public async Task Handle_Invoke_AllGood()
        {
            //Arrange
            _validationResultMock.Setup(v => v.IsValid).Returns(true);

            _updateAddressCommandValidatorMock
                .Setup(v => v.ValidateAsync(_updateAddressCommand, CancellationToken.None)).ReturnsAsync(_validationResultMock.Object);

            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var repository = new Mock<IBaseRepository<Address>>();

            _unitOfWorkMock.Setup(u => u.Repository<Address>()).Returns(repository.Object);

            var addressToUpdate = fixture.Build<Address>().Create();

            _unitOfWorkMock.Setup(u => u.Repository<Address>().GetByIdAsync(_updateAddressCommand.Id)).ReturnsAsync(addressToUpdate);

            _mapperMock.Setup(m => m.Map(_updateAddressCommand, addressToUpdate));

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync());

            //Act
            await new UpdateAddressCommandHandler
                (_mapperMock.Object, _unitOfWorkMock.Object, _updateAddressCommandValidatorMock.Object)
                .Handle(_updateAddressCommand, CancellationToken.None);

            //Assert
            _validationResultMock.Verify(v => v.IsValid, Times.Once);
            _updateAddressCommandValidatorMock.Verify(v => v.ValidateAsync(_updateAddressCommand, CancellationToken.None), Times.Once);
            _unitOfWorkMock.Verify(u => u.Repository<Address>(), Times.Once);
            repository.Verify(r => r.GetByIdAsync(_updateAddressCommand.Id), Times.Once);
            _mapperMock.Verify(m => m.Map(_updateAddressCommand, addressToUpdate), Times.Once);
        }

        [Fact]
        public async Task Handle_InvokeWithValidationErrors_ValidationExceptionOccures()
        {
            //Arrange
            _validationResultMock.Setup(v => v.IsValid).Returns(false);

            _updateAddressCommandValidatorMock
                .Setup(v => v.ValidateAsync(_updateAddressCommand, CancellationToken.None)).ReturnsAsync(_validationResultMock.Object);

            //Act and Assert
            Func<Task> act = async () => await new UpdateAddressCommandHandler(
                _mapperMock.Object, _unitOfWorkMock.Object, _updateAddressCommandValidatorMock.Object)
                .Handle(_updateAddressCommand, CancellationToken.None);

            await act.Should().ThrowAsync<ValidationException>();

            _validationResultMock.Verify(v => v.IsValid, Times.Once);
            _updateAddressCommandValidatorMock
                .Verify(v => v.ValidateAsync(_updateAddressCommand, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Handle_InvokeWithNotFoundAddress_NotFoundExceptionOccures()
        {
            //Arrange
            _validationResultMock.Setup(v => v.IsValid).Returns(true);

            _updateAddressCommandValidatorMock
                .Setup(v => v.ValidateAsync(_updateAddressCommand, CancellationToken.None)).ReturnsAsync(_validationResultMock.Object);

            var repository = new Mock<IBaseRepository<Address>>();
            repository.Setup(r => r.GetByIdAsync(_updateAddressCommand.Id)).ReturnsAsync(null as Address);

            _unitOfWorkMock.Setup(u => u.Repository<Address>()).Returns(repository.Object);

            //Act and Assert
            Func<Task> act = async () => await new UpdateAddressCommandHandler(
                _mapperMock.Object, _unitOfWorkMock.Object, _updateAddressCommandValidatorMock.Object)
                .Handle(_updateAddressCommand, CancellationToken.None);

            await act.Should().ThrowAsync<GloboDelivery.Application.Exceptions.NotFoundException>();

            _validationResultMock.Verify(v => v.IsValid, Times.Once);
            _updateAddressCommandValidatorMock
                .Verify(v => v.ValidateAsync(_updateAddressCommand, CancellationToken.None), Times.Once);
            _unitOfWorkMock.Verify(u => u.Repository<Address>(), Times.Once);
            repository.Verify(r => r.GetByIdAsync(_updateAddressCommand.Id), Times.Once);
        }
    }
}
