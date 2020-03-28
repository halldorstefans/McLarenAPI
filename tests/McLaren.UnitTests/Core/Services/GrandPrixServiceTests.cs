using McLaren.Core.Services;
using McLaren.UnitTests.Mocks.Repositories;
using McLaren.UnitTests.Mocks.Data;
using Moq;
using Xunit;

namespace McLaren.UnitTests.Core.Services
{
    public class GrandPrixServiceTests
    {
        [Fact]
        public async void GrandPrixService_GetAll_Valid()
        {
            var mockGrandPrix = MockGrandPrixData.GetListAsync();
                        
            var mockGrandPrixRepo = new MockGrandPrixRepository().MockGetAll(mockGrandPrix);
            var mockDriverRepo = new MockDriverRepository();
            var mockCarRepo = new MockCarRepository();
            var mockGrandPrixLoggerRepo = new MockLoggerRepository<GrandPrixService>();

            var mockGrandPrixService = new GrandPrixService(mockGrandPrixRepo.Object, mockDriverRepo.Object, mockCarRepo.Object, mockGrandPrixLoggerRepo.Object);

            var GrandPrixs = await mockGrandPrixService.GetAll();        

            Assert.NotEmpty(GrandPrixs);
            mockGrandPrixRepo.VerifyGetAllForGrandPrix(Times.Once());
        }        

        [Fact]
        public async void GrandPrixService_GetAll_NoGrandPrixs()
        {
            var mockGrandPrix = MockGrandPrixData.GetEmptyListAsync();
                        
            var mockGrandPrixRepo = new MockGrandPrixRepository().MockGetAll(mockGrandPrix);
            var mockDriverRepo = new MockDriverRepository();
            var mockCarRepo = new MockCarRepository();
            var mockGrandPrixLoggerRepo = new MockLoggerRepository<GrandPrixService>();

            var mockGrandPrixService = new GrandPrixService(mockGrandPrixRepo.Object, mockDriverRepo.Object, mockCarRepo.Object, mockGrandPrixLoggerRepo.Object);

            var GrandPrixs = await mockGrandPrixService.GetAll();        

            Assert.Empty(GrandPrixs);
            mockGrandPrixRepo.VerifyGetAllForGrandPrix(Times.Once());
        }

        [Fact]
        public async void GrandPrixService_GetById_ValidId()
        {
            var mockId = 2;
            var mockGrandPrix = MockGrandPrixData.GetSingleAsync();
                        
            var mockGrandPrixRepo = new MockGrandPrixRepository().MockGetById(mockGrandPrix);
            var mockDriverRepo = new MockDriverRepository();
            var mockCarRepo = new MockCarRepository();
            var mockGrandPrixLoggerRepo = new MockLoggerRepository<GrandPrixService>();

            var mockGrandPrixService = new GrandPrixService(mockGrandPrixRepo.Object, mockDriverRepo.Object, mockCarRepo.Object, mockGrandPrixLoggerRepo.Object);

            var GrandPrix = await mockGrandPrixService.GetById(mockId);        

            Assert.Equal(mockId, GrandPrix.id);
            mockGrandPrixRepo.VerifyGetByIdForGrandPrix(Times.Once());
        }        

        [Fact]
        public async void GrandPrixService_GetById_NoGrandPrixs()
        {
            var mockId = 1;
            var mockGrandPrix = MockGrandPrixData.GetSingleAsync();
                        
            var mockGrandPrixRepo = new MockGrandPrixRepository().MockGetById(mockGrandPrix);
            var mockDriverRepo = new MockDriverRepository();
            var mockCarRepo = new MockCarRepository();
            var mockGrandPrixLoggerRepo = new MockLoggerRepository<GrandPrixService>();

            var mockGrandPrixService = new GrandPrixService(mockGrandPrixRepo.Object, mockDriverRepo.Object, mockCarRepo.Object, mockGrandPrixLoggerRepo.Object);

            var GrandPrix = await mockGrandPrixService.GetById(mockId);        

            Assert.NotEqual(mockId, GrandPrix.id);
                       
            mockGrandPrixRepo.VerifyGetByIdForGrandPrix(Times.Once());
        }    

        [Fact]
        public async void GrandPrixService_GetByYear_ValidYear()
        {
            var mockGrandPrixYear = 1970;
            var mockGrandPrix = MockGrandPrixData.GetListAsync();
                        
            var mockGrandPrixRepo = new MockGrandPrixRepository().MockGetByYear(mockGrandPrix);
            var mockDriverRepo = new MockDriverRepository();
            var mockCarRepo = new MockCarRepository();
            var mockGrandPrixLoggerRepo = new MockLoggerRepository<GrandPrixService>();

            var mockGrandPrixService = new GrandPrixService(mockGrandPrixRepo.Object, mockDriverRepo.Object, mockCarRepo.Object, mockGrandPrixLoggerRepo.Object);

            var GrandPrixs = await mockGrandPrixService.GetByYear(mockGrandPrixYear);        

            Assert.NotEmpty(GrandPrixs);
            mockGrandPrixRepo.VerifyGetByYearForGrandPrix(Times.Exactly(2));
        }        

        [Fact]
        public async void GrandPrixService_GetByYear_NoGrandPrixs()
        {
            var mockGrandPrixYear = 2000;
            var mockGrandPrix = MockGrandPrixData.GetListAsync();
                        
            var mockGrandPrixRepo = new MockGrandPrixRepository().MockGetByYear(mockGrandPrix);
            var mockDriverRepo = new MockDriverRepository();
            var mockCarRepo = new MockCarRepository();
            var mockGrandPrixLoggerRepo = new MockLoggerRepository<GrandPrixService>();

            var mockGrandPrixService = new GrandPrixService(mockGrandPrixRepo.Object, mockDriverRepo.Object, mockCarRepo.Object, mockGrandPrixLoggerRepo.Object);

            var GrandPrixs = await mockGrandPrixService.GetByYear(mockGrandPrixYear);        

            foreach (var GrandPrix in GrandPrixs)
            {
                Assert.NotEqual(mockGrandPrixYear, GrandPrix.year);
            } 
            mockGrandPrixRepo.VerifyGetByYearForGrandPrix(Times.Exactly(2));
        }    
    }
}