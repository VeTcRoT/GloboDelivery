using AutoMapper;
using Bogus;
using FluentAssertions;
using GloboDelivery.Application.Exceptions;
using GloboDelivery.Application.Features.Addresses.Queries.GetAddressById;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using Moq;

namespace Application.UnitTests.Features.Addresses.Queries
{
    public class GetAddressByIdQueryHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IBaseRepository<Address>> _repositoryMock;
        private readonly GetAddressByIdQuery _getAddressByIdQuery;

        public GetAddressByIdQueryHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IBaseRepository<Address>>();

            var getAddressByIdQueryFaker = new Faker<GetAddressByIdQuery>()
                .RuleFor(q => q.Id, f => f.Random.Number(1, 100));

            _getAddressByIdQuery = getAddressByIdQueryFaker.Generate();
        }

        [Fact]
        public async Task Handle_Invoke_AllGood()
        {
            //Arrange
            _unitOfWorkMock.Setup(u => u.Repository<Address>()).Returns(_repositoryMock.Object);

            var address = new Address();
            _repositoryMock.Setup(r => r.GetByIdAsync(_getAddressByIdQuery.Id)).ReturnsAsync(address);

            var addressToReturn = new AddressDto();
            _mapperMock.Setup(m => m.Map<AddressDto>(address)).Returns(addressToReturn);

            //Act
            var result = await new GetAddressByIdQueryHandler(_mapperMock.Object, _unitOfWorkMock.Object)
                .Handle(_getAddressByIdQuery, CancellationToken.None);

            //Assert
            _unitOfWorkMock.Verify(u => u.Repository<Address>(), Times.Once);
            _repositoryMock.Verify(r => r.GetByIdAsync(_getAddressByIdQuery.Id), Times.Once);
            _mapperMock.Verify(m => m.Map<AddressDto>(address), Times.Once);

            result.Should().BeEquivalentTo(addressToReturn);
        }

        [Fact]
        public async Task Handle_InvokeWithNotFoundAddress_NotFoundExceptionOccures()
        {
            //Arrange
            _unitOfWorkMock.Setup(u => u.Repository<Address>()).Returns(_repositoryMock.Object);

            _repositoryMock.Setup(r => r.GetByIdAsync(_getAddressByIdQuery.Id)).ReturnsAsync(null as Address);

            //Act and Assert
            Func<Task> act = async () => await new GetAddressByIdQueryHandler(_mapperMock.Object, _unitOfWorkMock.Object)
                .Handle(_getAddressByIdQuery, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>().WithMessage($"{nameof(Address)} ({_getAddressByIdQuery.Id}) is not found");
            _unitOfWorkMock.Verify(u => u.Repository<Address>(), Times.Once);
            _repositoryMock.Verify(r => r.GetByIdAsync(_getAddressByIdQuery.Id), Times.Once);
        }
    }
}
