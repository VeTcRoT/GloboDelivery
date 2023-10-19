using AutoFixture;
using AutoMapper;
using FluentAssertions;
using GloboDelivery.Application.Exceptions;
using GloboDelivery.Application.Features.Addresses.Queries.GetDeliveryAddresses;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Helpers;
using GloboDelivery.Domain.Interfaces;
using Moq;

namespace Application.UnitTests.Features.Addresses.Queries
{
    public class GetDeliveryAddressesQueryHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IDeliveryRepository> _deliveryRepositoiryMock;
        private readonly GetDeliveryAddressesQuery _getDeliveryAddressesQuery;
        private readonly Fixture _fixture;

        public GetDeliveryAddressesQueryHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _deliveryRepositoiryMock = new Mock<IDeliveryRepository>();
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _getDeliveryAddressesQuery = _fixture.Build<GetDeliveryAddressesQuery>().Create();
        }

        [Fact]
        public async Task Handle_Invoke_AllGood()
        {
            //Arrange
            _unitOfWorkMock.Setup(u => u.DeliveryRepository).Returns(_deliveryRepositoiryMock.Object);

            var pagedAddresses = _fixture.Build<PagedList<Address>>().Create();
            _deliveryRepositoiryMock
                .Setup(r => r.GetPagedDeliveryAddressesAsync(
                    _getDeliveryAddressesQuery.DeliveryId, _getDeliveryAddressesQuery.PageNumber, _getDeliveryAddressesQuery.PageSize))
                .ReturnsAsync(pagedAddresses);

            var pagedAddressesDto = _fixture.Build<PagedList<AddressDto>>().Create();
            _mapperMock.Setup(m => m.Map<PagedList<AddressDto>>(pagedAddresses)).Returns(pagedAddressesDto);

            //Act
            var result = await new GetDeliveryAddressesQueryHandler(_mapperMock.Object, _unitOfWorkMock.Object)
                .Handle(_getDeliveryAddressesQuery, CancellationToken.None);

            //Assert
            _unitOfWorkMock.Verify(u => u.DeliveryRepository, Times.Once);
            _deliveryRepositoiryMock
                .Verify(r => r.GetPagedDeliveryAddressesAsync(
                    _getDeliveryAddressesQuery.DeliveryId, _getDeliveryAddressesQuery.PageNumber, _getDeliveryAddressesQuery.PageSize), Times.Once);
            _mapperMock.Verify(m => m.Map<PagedList<AddressDto>>(pagedAddresses), Times.Once);

            result.Should().BeEquivalentTo(pagedAddressesDto);
        }

        [Fact]
        public async Task Handle_InvokeWithNotFoundDeliveryId_NotFoundExceptionOccures()
        {
            //Arrange
            _unitOfWorkMock.Setup(u => u.DeliveryRepository).Returns(_deliveryRepositoiryMock.Object);

            _deliveryRepositoiryMock
                .Setup(r => r.GetPagedDeliveryAddressesAsync(
                    _getDeliveryAddressesQuery.DeliveryId, _getDeliveryAddressesQuery.PageNumber, _getDeliveryAddressesQuery.PageSize))
                .ReturnsAsync(null as PagedList<Address>);

            //Act and Assert
            Func<Task> act = async() => await new GetDeliveryAddressesQueryHandler(_mapperMock.Object, _unitOfWorkMock.Object)
                .Handle(_getDeliveryAddressesQuery, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>().WithMessage($"{nameof(Delivery)} ({_getDeliveryAddressesQuery.DeliveryId}) is not found");

            _unitOfWorkMock.Verify(u => u.DeliveryRepository, Times.Once);
            _deliveryRepositoiryMock.Verify(r => r.GetPagedDeliveryAddressesAsync(
                _getDeliveryAddressesQuery.DeliveryId, _getDeliveryAddressesQuery.PageNumber, _getDeliveryAddressesQuery.PageSize), Times.Once);
        }
    }
}
