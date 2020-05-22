using Malots.WebAPI.Domain.Enums;
using Malots.WebAPI.Domain.Interfaces.Business;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Malots.WebAPI.Tests.Domain
{
    public sealed class IBaseLogicTest
    {
        private readonly Mock<IBaseLogic<IWorkModel>> _mockedLogic;

        public IBaseLogicTest()
        {
            _mockedLogic = new Mock<IBaseLogic<IWorkModel>>();
        }

        [Fact]
        public async Task GetByQuantityTest()
        {
            //arrange
            _mockedLogic.Setup(x => x.Get(It.IsAny<QueryTakeEnum>(), It.IsAny<QuerySkipEnum>())).ReturnsAsync(new List<IWorkModel>() { new Mock<IWorkModel>().Object });

            //act
            var getResult = await _mockedLogic.Object.Get(QueryTakeEnum.One, QuerySkipEnum.None);
            var result = getResult.ToArray();

            //assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdTest()
        {
            //arrange
            _mockedLogic.Setup(x => x.Get(It.IsAny<Guid>())).ReturnsAsync(new Mock<IWorkModel>().Object);

            //act
            var result = await _mockedLogic.Object.Get(new Guid());

            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IWorkModel>(result);
        }

        [Fact]
        public async Task PostBulkModelTest()
        {
            //arrange
            var mockedModel = new Mock<IWorkModel>();
            var mockedList = new List<IWorkModel>() { mockedModel.Object };
            _mockedLogic.Setup(x => x.Post(It.IsAny<IEnumerable<IWorkModel>>())).ReturnsAsync(new List<Guid>() { new Guid() });

            //act
            var resultList = await _mockedLogic.Object.Post(mockedList);
            var result = resultList.ToArray();

            //assert
            Assert.Single(result);
            Assert.IsAssignableFrom<Guid[]>(result);
        }

        [Fact]
        public async Task PostModelTest()
        {
            //arrange
            var mockedModel = new Mock<IWorkModel>();
            _mockedLogic.Setup(x => x.Post(It.IsAny<IWorkModel>())).ReturnsAsync(new Guid());

            //act
            var result = await _mockedLogic.Object.Post(mockedModel.Object);

            //assert
            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task PutBulkModelTest()
        {
            //arrange
            var mockedModelList = new Mock<IEnumerable<IWorkModel>>();
            _mockedLogic.Setup(x => x.Put(It.IsAny<IEnumerable<IWorkModel>>())).ReturnsAsync(new Random().Next(1, int.MaxValue));

            //act
            var result = await _mockedLogic.Object.Put(mockedModelList.Object);

            //assert
            Assert.True(result > 0);
        }

        [Fact]
        public async Task PutModelTest()
        {
            //arrange
            var mockedModel = new Mock<IWorkModel>();
            _mockedLogic.Setup(x => x.Put(It.IsAny<IWorkModel>())).ReturnsAsync(new Random().Next(1, int.MaxValue));

            //act
            var result = await _mockedLogic.Object.Put(mockedModel.Object);

            //assert
            Assert.True(result > 0);
        }

        [Fact]
        public async Task DeleteModelTest()
        {
            var guid = new Guid();
            _mockedLogic.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(new Random().Next(1, int.MaxValue));

            //act
            var result = await _mockedLogic.Object.Delete(guid);

            //assert
            Assert.True(result > 0);
        }
    }
}
