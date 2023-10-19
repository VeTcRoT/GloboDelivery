using AutoFixture;
using AutoMapper;
using FluentAssertions;
using GloboDelivery.Application.Exceptions;
using GloboDelivery.Application.Features.VanInfos.Queries.GetDeliveryVanInfo;
using GloboDelivery.Application.Features.VanInfos.Queries.GetVanInfoById;
using GloboDelivery.Domain.Dtos;
using GloboDelivery.Domain.Entities;
using GloboDelivery.Domain.Interfaces;
using Moq;

namespace Application.UnitTests.Features.VanInfos.Queries
{
    public class GetVanInfoByIdQueryHandlerTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IBaseRepository<VanInfo>> _repositoryMock;
        private readonly Fixture _fixture;
        private readonly GetVanInfoByIdQuery _getVanInfoByIdQuery;

        public GetVanInfoByIdQueryHandlerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IBaseRepository<VanInfo>>();
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _getVanInfoByIdQuery = new GetVanInfoByIdQuery { Id = 1 };
        }

        [Fact]
        public async Task Handle_Invoke_AllGood()
        {
            //Arrange
            var vanInfoFromRepo = _fixture.Build<VanInfo>().Create();
            _repositoryMock.Setup(r => r.GetByIdAsync(_getVanInfoByIdQuery.Id)).ReturnsAsync(vanInfoFromRepo);

            _unitOfWorkMock.Setup(u => u.Repository<VanInfo>()).Returns(_repositoryMock.Object);

            var vanInfoToReturn = _fixture.Build<VanInfoDto>().Create();
            _mapperMock.Setup(m => m.Map<VanInfoDto>(vanInfoFromRepo)).Returns(vanInfoToReturn);

            //Act
            var result = await new GetVanInfoByIdQueryHandler(_mapperMock.Object, _unitOfWorkMock.Object)
                .Handle(_getVanInfoByIdQuery, CancellationToken.None);

            //Assert
            _repositoryMock.Verify(r => r.GetByIdAsync(_getVanInfoByIdQuery.Id), Times.Once);
            _unitOfWorkMock.Verify(u => u.Repository<VanInfo>(), Times.Once);
            _mapperMock.Verify(m => m.Map<VanInfoDto>(vanInfoFromRepo), Times.Once);

            result.Should().BeEquivalentTo(vanInfoToReturn);
        }

        [Fact]
        public async Task Handle_InvokeWithNotFoundVanInfo()
        {
            //Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync(_getVanInfoByIdQuery.Id)).ReturnsAsync(null as VanInfo);

            _unitOfWorkMock.Setup(u => u.Repository<VanInfo>()).Returns(_repositoryMock.Object);

            //Act and Assert
            Func<Task> act = async() => await new GetVanInfoByIdQueryHandler(_mapperMock.Object, _unitOfWorkMock.Object)
            .Handle(_getVanInfoByIdQuery, CancellationToken.None);

            await act.Should().ThrowAsync<NotFoundException>().WithMessage($"{nameof(VanInfo)} ({_getVanInfoByIdQuery.Id}) is not found");

            _repositoryMock.Verify(r => r.GetByIdAsync(_getVanInfoByIdQuery.Id), Times.Once);
            _unitOfWorkMock.Verify(u => u.Repository<VanInfo>(), Times.Once);
        }
    }
}
