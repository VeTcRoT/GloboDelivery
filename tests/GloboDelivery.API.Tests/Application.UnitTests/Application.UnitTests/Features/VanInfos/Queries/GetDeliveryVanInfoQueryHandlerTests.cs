using AutoFixture;
using AutoMapper;
using FluentAssertions;
using GloboDelivery.Application.Exceptions;
using GloboDelivery.Application.Features.VanInfos.Queries.GetDeliveryVanInfo;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using Moq;

namespace Application.UnitTests.Features.VanInfos.Queries
{
    public class GetDeliveryVanInfoQueryHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IDeliveryRepository> _repositoryMock;
        private readonly Fixture _fixture;
        private readonly GetDeliveryVanInfoQuery _getDeliveryVanInfoQuery;

        public GetDeliveryVanInfoQueryHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IDeliveryRepository>();
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _getDeliveryVanInfoQuery = _fixture.Build<GetDeliveryVanInfoQuery>().Create();
        }

        [Fact]
        public async Task Handle_Invoke_AllGood()
        {
            //Arrange
            var vanInfoFromRepo = _fixture.Build<VanInfo>().Create();
            _repositoryMock.Setup(r => r.GetDeliveryVanInfoAsync(_getDeliveryVanInfoQuery.DeliveryId)).ReturnsAsync(vanInfoFromRepo);

            _unitOfWorkMock.Setup(u => u.DeliveryRepository).Returns(_repositoryMock.Object);

            var vanInfoToReturn = _fixture.Build<VanInfoDto>().Create();
            _mapperMock.Setup(m => m.Map<VanInfoDto>(vanInfoFromRepo)).Returns(vanInfoToReturn);

            //Act
            var result = await new GetDeliveryVanInfoQueryHandler(_mapperMock.Object, _unitOfWorkMock.Object)
                .Handle(_getDeliveryVanInfoQuery, CancellationToken.None);

            //Assert
            _repositoryMock.Verify(r => r.GetDeliveryVanInfoAsync(_getDeliveryVanInfoQuery.DeliveryId), Times.Once);
            _unitOfWorkMock.Verify(u => u.DeliveryRepository, Times.Once);
            _mapperMock.Verify(m => m.Map<VanInfoDto>(vanInfoFromRepo), Times.Once);

            result.Should().BeEquivalentTo(vanInfoToReturn);
        }

        [Fact]
        public async Task Handle_InvokeWithNotFoundDelivery_NotFoundEceptionOccures()
        {
            //Arrange
            _repositoryMock.Setup(r => r.GetDeliveryVanInfoAsync(_getDeliveryVanInfoQuery.DeliveryId)).ReturnsAsync(null as VanInfo);

            _unitOfWorkMock.Setup(u => u.DeliveryRepository).Returns(_repositoryMock.Object);

            //Act and Assert
            Func<Task> act = async() => await new GetDeliveryVanInfoQueryHandler(_mapperMock.Object, _unitOfWorkMock.Object)
                .Handle(_getDeliveryVanInfoQuery, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>().WithMessage($"{nameof(Delivery)} ({_getDeliveryVanInfoQuery.DeliveryId}) is not found");

            _repositoryMock.Verify(r => r.GetDeliveryVanInfoAsync(_getDeliveryVanInfoQuery.DeliveryId), Times.Once);
            _unitOfWorkMock.Verify(u => u.DeliveryRepository, Times.Once);
        }
    }
}
