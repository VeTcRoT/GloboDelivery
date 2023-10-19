using AutoFixture;
using AutoMapper;
using Bogus;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using GloboDelivery.Application.Features.Addresses.Commands.CreateAddress;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using Moq;

namespace Application.UnitTests.Features.Addresses.Commands
{
    public class CreateAddressCommandHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidator<CreateAddressCommand>> _createAddressCommandValidatorMock;
        private readonly CreateAddressCommand _createAddressCommand;

        public CreateAddressCommandHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _createAddressCommandValidatorMock = new Mock<IValidator<CreateAddressCommand>>();

            var createAddressCommandFaker = new Faker<CreateAddressCommand>()
                .RuleFor(c => c.Country, f => f.Address.County())
                .RuleFor(c => c.City, f => f.Address.City())
                .RuleFor(c => c.AdministrativeArea, f => f.Address.State())
                .RuleFor(c => c.AddressLine, f => f.Address.StreetAddress())
                .RuleFor(c => c.SuiteNumber, f => f.Random.Number(1, 10))
                .RuleFor(c => c.PostalCode, f => f.Address.ZipCode());

            _createAddressCommand = createAddressCommandFaker.Generate();
        }

        [Fact]
        public async Task Handle_Invoke_AllGood()
        {
            // Arrange
            var validationResult = new Mock<FluentValidation.Results.ValidationResult>();
            validationResult.Setup(v => v.IsValid).Returns(true);

            _createAddressCommandValidatorMock.Setup(v => v.ValidateAsync(_createAddressCommand, CancellationToken.None)).ReturnsAsync(validationResult.Object);

            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var addressToAdd = fixture.Build<Address>().Create();

            _mapperMock.Setup(m => m.Map<Address>(_createAddressCommand)).Returns(addressToAdd);

            var repository = new Mock<IBaseRepository<Address>>();

            _unitOfWorkMock.Setup(u => u.Repository<Address>()).Returns(repository.Object);

            repository.Setup(r => r.CreateAsync(addressToAdd));

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync());

            var addressToReturn = fixture.Build<AddressDto>().Create();

            _mapperMock.Setup(m => m.Map<AddressDto>(addressToAdd)).Returns(addressToReturn);

            // Act
            var result = await new CreateAddressCommandHandler(
                _mapperMock.Object, _unitOfWorkMock.Object, _createAddressCommandValidatorMock.Object)
                .Handle(_createAddressCommand, CancellationToken.None);

            // Assert
            _mapperMock.Verify(m => m.Map<Address>(_createAddressCommand), Times.Once);
            _mapperMock.Verify(m => m.Map<AddressDto>(addressToAdd), Times.Once);

            repository.Verify(r => r.CreateAsync(addressToAdd), Times.Once);

            _unitOfWorkMock.Verify(u => u.Repository<Address>(), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);

            result.Should().BeEquivalentTo(addressToReturn);
        }

        [Fact]
        public async Task Handle_InvokeWithValidationError_ReturnsValidationException()
        {
            // Arrange
            var validationResult = new Mock<FluentValidation.Results.ValidationResult>();
            validationResult.Setup(v => v.IsValid).Returns(false);

            _createAddressCommandValidatorMock
                .Setup(v => v.ValidateAsync(_createAddressCommand, CancellationToken.None)).ReturnsAsync(validationResult.Object);

            // Act and Assert
            Func<Task> act = async () => await new CreateAddressCommandHandler(
                _mapperMock.Object, _unitOfWorkMock.Object, _createAddressCommandValidatorMock.Object)
                .Handle(_createAddressCommand, CancellationToken.None);

            await act.Should().ThrowAsync<GloboDelivery.Application.Exceptions.ValidationException>();
        }
    }
}
