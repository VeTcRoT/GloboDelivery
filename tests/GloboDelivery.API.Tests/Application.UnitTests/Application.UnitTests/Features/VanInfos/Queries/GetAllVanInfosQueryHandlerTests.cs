using AutoFixture;
using AutoMapper;
using FluentAssertions;
using GloboDelivery.Application.Features.VanInfos.Queries.GetAllVanInfos;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Helpers;
using GloboDelivery.Domain.Interfaces;
using Moq;

namespace Application.UnitTests.Features.VanInfos.Queries
{
    public class GetAllVanInfosQueryHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IBaseRepository<VanInfo>> _repositoryMock;
        private readonly Fixture _fixture;
        private readonly GetAllVanInfosQuery _getAllVanInfosQuery;

        public GetAllVanInfosQueryHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IBaseRepository<VanInfo>>();
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _getAllVanInfosQuery = _fixture.Build<GetAllVanInfosQuery>().Create();
        }

        [Fact]
        public async Task Handle_Invoke_AllGood()
        {
            //Arrange
            var vanInfosFromRepo = _fixture.Build<PagedList<VanInfo>>().Create();
            _repositoryMock.Setup(r => r.ListPagedAsync(_getAllVanInfosQuery.PageNumber, _getAllVanInfosQuery.PageSize)).ReturnsAsync(vanInfosFromRepo);

            _unitOfWorkMock.Setup(u => u.Repository<VanInfo>()).Returns(_repositoryMock.Object);

            var vanInfosToReturn = _fixture.Build<PagedList<VanInfoDto>>().Create();
            _mapperMock.Setup(m => m.Map<PagedList<VanInfoDto>>(vanInfosFromRepo)).Returns(vanInfosToReturn);

            //Act
            var result = await new GetAllVanInfosQueryHandler(_mapperMock.Object, _unitOfWorkMock.Object)
                .Handle(_getAllVanInfosQuery, CancellationToken.None);

            //Assert
            _repositoryMock.Verify(r => r.ListPagedAsync(_getAllVanInfosQuery.PageNumber, _getAllVanInfosQuery.PageSize), Times.Once);
            _unitOfWorkMock.Verify(u => u.Repository<VanInfo>(), Times.Once);
            _mapperMock.Verify(m => m.Map<PagedList<VanInfoDto>>(vanInfosFromRepo), Times.Once);

            result.Should().BeEquivalentTo(vanInfosToReturn);
        }
    }
}
