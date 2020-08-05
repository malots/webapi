using Malots.WebAPI.Domain.Enums;
using Malots.WebAPI.Domain.Interfaces.Application;
using Malots.WebAPI.Domain.Interfaces.Business;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Malots.WebAPI.Tests.Domain
{
    public class IBaseControllerTest
    {
        private readonly Mock<IBaseController<IViewModel, IWorkModel>> _mockedController;

        public IBaseControllerTest()
        {
            _mockedController = new Mock<IBaseController<IViewModel, IWorkModel>>();
        }

        [Fact]
        public async Task GetByQuantityTest()
        {
            //arrange
            _mockedController.Setup(x => x.Get(It.IsAny<QueryTakeEnum>(), It.IsAny<QuerySkipEnum>(), true)).ReturnsAsync(new List<IViewModel>() { new Mock<IViewModel>().Object });

            //act
            var getResult = await _mockedController.Object.Get(QueryTakeEnum.One, QuerySkipEnum.None);
            var result = getResult.ToArray();

            //assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdTest()
        {
            //arrange
            _mockedController.Setup(x => x.Get(It.IsAny<string>(), true)).ReturnsAsync(new Mock<IViewModel>().Object);

            //act
            var result = await _mockedController.Object.Get(new Guid().ToString());

            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IViewModel>(result);
        }

        [Fact]
        public async Task PostModelTest()
        {
            //arrange
            var mockedViewModel = new Mock<IViewModel>();
            _mockedController.Setup(x => x.Post(It.IsAny<IViewModel>())).ReturnsAsync(new Guid().ToString());

            //act
            var result = await _mockedController.Object.Post(mockedViewModel.Object);

            //assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<string>(result);
        }

        [Fact]
        public async Task PostBulkModelTest()
        {
            //arrange
            var mockedViewModel = new Mock<IViewModel>();
            var mockedList = new List<IViewModel> { mockedViewModel.Object };
            _mockedController.Setup(x => x.Post(It.IsAny<IEnumerable<IViewModel>>())).ReturnsAsync(new List<string> { new Guid().ToString() });

            //act
            var resultList = await _mockedController.Object.Post(mockedList);
            var result = resultList.ToArray();

            //assert
            Assert.Single(result);
            Assert.IsAssignableFrom<string[]>(result);
        }

        [Fact]
        public async Task PutModelTest()
        {
            //arrange
            var mockedViewModel = new Mock<IViewModel>();
            _mockedController.Setup(x => x.Put(It.IsAny<string>(), It.IsAny<IViewModel>())).ReturnsAsync(new Random().Next(1, int.MaxValue));

            //act
            var result = await _mockedController.Object.Put(new Guid().ToString(), mockedViewModel.Object);

            //assert
            Assert.True(result > 0);
        }

        [Fact]
        public async Task DeleteModelTest()
        {
            //arrange
            var mockedViewModel = new Mock<IViewModel>();
            _mockedController.Setup(x => x.Delete(It.IsAny<String>())).ReturnsAsync(new Random().Next(1, int.MaxValue));

            //act
            var result = await _mockedController.Object.Delete(mockedViewModel.Object.Id);

            //assert
            Assert.True(result > 0);
        }
    }
}
