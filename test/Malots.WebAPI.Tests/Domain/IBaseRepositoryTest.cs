using Malots.WebAPI.Domain.Interfaces.Infra;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Malots.WebAPI.Tests.Domain
{
    public sealed class IRepositoryTest
    {
        private readonly Mock<IBaseRepository<IRepositoryModel>> _mockedRepository;

        public IRepositoryTest()
        {
            _mockedRepository = new Mock<IBaseRepository<IRepositoryModel>>();
        }

        [Fact]
        public void SelectAllTest()
        {
            //arrange
            _mockedRepository.Setup(x => x.Select()).Returns(new List<IRepositoryModel>() { new Mock<IRepositoryModel>().Object }.AsQueryable());

            //act
            var result = _mockedRepository.Object.Select().ToArray();

            //assert
            Assert.Single(result);
        }

        [Fact]
        public void SelectByIdTest()
        {
            //arrange
            _mockedRepository.Setup(x => x.Select(It.IsAny<Guid>())).Returns(new List<IRepositoryModel>() { new Mock<IRepositoryModel>().Object }.AsQueryable());

            //act
            var result = _mockedRepository.Object.Select(new Guid()).ToArray();

            //assert
            Assert.Single(result);
        }


        [Fact]
        public void InsertOneEntityAndGetByIdTest()
        {
            //arrange
            var mockedEntity = new Mock<IRepositoryModel>();
            _mockedRepository.Setup(x => x.Insert(It.IsAny<IRepositoryModel>())).Returns(new Guid());

            //act
            var guid = _mockedRepository.Object.Insert(mockedEntity.Object);

            //assert
            Assert.IsType<Guid>(guid);
        }

        [Fact]
        public void InsertBulkEntityAndGetAllTest()
        {
            //arrange
            var mockedEntityList = new Mock<IEnumerable<IRepositoryModel>>();
            _mockedRepository.Setup(x => x.Insert(It.IsAny<IEnumerable<IRepositoryModel>>())).Returns(new List<Guid>() { new Guid() });

            //act
            var guidList = _mockedRepository.Object.Insert(mockedEntityList.Object);

            //assert
            Assert.IsAssignableFrom<IEnumerable<Guid>>(guidList);
        }

        [Fact]
        public void UpdateOneEntityAndGetByIdTest()
        {
            //arrange
            var mockedEntity = new Mock<IRepositoryModel>();
            _mockedRepository.Setup(x => x.Update(It.IsAny<IRepositoryModel>())).Verifiable();

            //act
            _mockedRepository.Object.Update(mockedEntity.Object);

            //assert
            _mockedRepository.Verify(x => x.Update(It.IsAny<IRepositoryModel>()), Times.Once);
        }

        [Fact]
        public void UpdateBulkEntityAndGetAllTest()
        {
            //arrange
            var mockedEntityList = new Mock<IEnumerable<IRepositoryModel>>();
            _mockedRepository.Setup(x => x.Update(It.IsAny<IEnumerable<IRepositoryModel>>())).Verifiable();


            //act
            _mockedRepository.Object.Update(mockedEntityList.Object);

            //assert
            _mockedRepository.Verify(x => x.Update(It.IsAny<IEnumerable<IRepositoryModel>>()), Times.Once);
        }

        [Fact]
        public void DeleteEntityTest()
        {
            //arrange
            var guid = new Guid();
            _mockedRepository.Setup(x => x.Delete(It.IsAny<Guid>())).Verifiable();

            //act
            _mockedRepository.Object.Delete(guid);

            //assert
            _mockedRepository.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task SaveAsyncTest()
        {
            //arrange
            _mockedRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(new Random().Next);
            
            //act
            var result = await _mockedRepository.Object.SaveChangesAsync().ConfigureAwait(false);

            //assert
            Assert.IsType<int>(result);
        }

        [Fact]
        public void SaveSyncTest()
        {
            //arrange
            _mockedRepository.Setup(x => x.SaveChanges()).Verifiable();

            //act
            var result = _mockedRepository.Object.SaveChanges();

            //assert
            Assert.IsType<int>(result);
        }
    }
}
