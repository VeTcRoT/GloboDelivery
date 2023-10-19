using AutoFixture;
using AutoMapper;
using FluentAssertions;
using GloboDelivery.Application.Features.Addresses.Queries.GetAllAddresses;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Helpers;
using GloboDelivery.Domain.Interfaces;
using Moq;

namespace Application.UnitTests.Features.Addresses.Queries
{
    public class GetAllAddressesQueryHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfworkMock;
        private readonly Mock<IAddressRepository> _addressRepositoryMock;
        private readonly GetAllAddressesQuery _getAllAddressesQuery;
        private readonly Fixture _fixture;

        public GetAllAddressesQueryHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _unitOfworkMock = new Mock<IUnitOfWork>();
            _addressRepositoryMock = new Mock<IAddressRepository>();

            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _getAllAddressesQuery = _fixture.Build<GetAllAddressesQuery>().Create();
        }

        [Fact]
        public async Task Handle_Invoke_AllGood()
        {
            //Arrange
            _unitOfworkMock.Setup(u => u.AddressRepository).Returns(_addressRepositoryMock.Object);

            var pagedAddresses = _fixture.Build<PagedList<Address>>().Create();
            _addressRepositoryMock.Setup(r => r.ListPagedAsync(_getAllAddressesQuery.PageNumber, _getAllAddressesQuery.PageSize)).ReturnsAsync(pagedAddresses);

            var pagedAddressesDto = _fixture.Build<PagedList<AddressDto>>().Create();
            _mapperMock.Setup(m => m.Map<PagedList<AddressDto>>(pagedAddresses)).Returns(pagedAddressesDto);

            //Act
            var result = await new GetAllAddressesQueryHandler(_mapperMock.Object, _unitOfworkMock.Object)
                .Handle(_getAllAddressesQuery, CancellationToken.None);

            //Assert
            _unitOfworkMock.Verify(u => u.AddressRepository, Times.Once);
            _addressRepositoryMock.Verify(r => r.ListPagedAsync(_getAllAddressesQuery.PageNumber, _getAllAddressesQuery.PageSize), Times.Once);
            _mapperMock.Verify(m => m.Map<PagedList<AddressDto>>(pagedAddresses), Times.Once);

            result.Should().BeEquivalentTo(pagedAddressesDto);
        }
    }
}
