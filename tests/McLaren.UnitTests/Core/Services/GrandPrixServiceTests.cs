using McLaren.Core.Services;
using McLaren.UnitTests.Mocks.Repositories;
using McLaren.UnitTests.Mocks.Data;
using Moq;
using Xunit;
using McLaren.Core.ResourceParameters;
using System;

namespace McLaren.UnitTests.Core.Services
{
    public class GrandsPrixServiceTests
    {
        [Fact]
        public async void GrandsPrixService_GetAll_Valid()
        {
            // Arrange
            var mockGrandPrix = MockGrandPrixData.GetAllEntitiesListAsync();                        
            var mockGrandPrixRepo = new MockGrandPrixRepository().MockGetAll(mockGrandPrix);
            var mockDriverRepo = new MockDriverRepository();
            var mockCarRepo = new MockCarRepository();
            var mockGrandPrixLoggerRepo = new MockLoggerRepository<GrandPrixesService>();
            var mockGrandsPrixService = new GrandPrixesService(mockGrandPrixRepo.Object, mockDriverRepo.Object, mockCarRepo.Object, mockGrandPrixLoggerRepo.Object);

            // Act
            var GrandPrixs = await mockGrandsPrixService.GetGrandPrixes();

            // Assert
            Assert.NotEmpty(GrandPrixs);
            mockGrandPrixRepo.VerifyGetAllForGrandPrix(Times.Once());
        }        

        [Fact]
        public async void GrandsPrixService_GetAll_NoGrandPrixs()
        {
            // Arrange
            var mockGrandPrix = MockGrandPrixData.GetEmptyEntityListAsync();                        
            var mockGrandPrixRepo = new MockGrandPrixRepository().MockGetAll(mockGrandPrix);
            var mockDriverRepo = new MockDriverRepository();
            var mockCarRepo = new MockCarRepository();
            var mockGrandPrixLoggerRepo = new MockLoggerRepository<GrandPrixesService>();
            var mockGrandsPrixService = new GrandPrixesService(mockGrandPrixRepo.Object, mockDriverRepo.Object, mockCarRepo.Object, mockGrandPrixLoggerRepo.Object);

            // Act
            var GrandPrixs = await mockGrandsPrixService.GetGrandPrixes();        

            // Assert
            Assert.Null(GrandPrixs);
            mockGrandPrixRepo.VerifyGetAllForGrandPrix(Times.Once());
        }

        [Fact]
        public async void GrandsPrixService_GetAllFilter_Valid()
        {
            // Arrange
            var mockGrandPrix = MockGrandPrixData.GetAllEntitiesListAsync();
            GrandPrixesResourceParameters parameters = new GrandPrixesResourceParameters{Country = "Spain"};                        
            var mockGrandPrixRepo = new MockGrandPrixRepository().MockGetByCountry(mockGrandPrix);
            var mockDriverRepo = new MockDriverRepository();
            var mockCarRepo = new MockCarRepository();
            var mockGrandPrixLoggerRepo = new MockLoggerRepository<GrandPrixesService>();
            var mockGrandsPrixService = new GrandPrixesService(mockGrandPrixRepo.Object, mockDriverRepo.Object, mockCarRepo.Object, mockGrandPrixLoggerRepo.Object);

            // Act
            var GrandPrixes = await mockGrandsPrixService.GetGrandPrixes(parameters);       

            // Assert
            Assert.NotEmpty(GrandPrixes);
            mockGrandPrixRepo.VerifyGetByCountryForGrandPrix(Times.Once());
        }        

        [Fact]
        public async void GrandsPrixService_GetAllFilter_NoGrandPrixs()
        {
            // Arrange
            var mockGrandPrix = MockGrandPrixData.GetEmptyEntityListAsync();
            GrandPrixesResourceParameters parameters = new GrandPrixesResourceParameters{Country = "USA"};                        
            var mockGrandPrixRepo = new MockGrandPrixRepository().MockGetByCountry(mockGrandPrix);
            var mockDriverRepo = new MockDriverRepository();
            var mockCarRepo = new MockCarRepository();
            var mockGrandPrixLoggerRepo = new MockLoggerRepository<GrandPrixesService>();
            var mockGrandsPrixService = new GrandPrixesService(mockGrandPrixRepo.Object, mockDriverRepo.Object, mockCarRepo.Object, mockGrandPrixLoggerRepo.Object);

            // Act
            var GrandPrixs = await mockGrandsPrixService.GetGrandPrixes(parameters);        

            // Assert
            Assert.Empty(GrandPrixs);
            mockGrandPrixRepo.VerifyGetByCountryForGrandPrix(Times.Once());
        }

        [Fact]
        public async void GrandsPrixService_GetById_ValidId()
        {
            // Arrange
            var mockId = 15;
            var mockGrandPrix = MockGrandPrixData.GetAllEntitiesListAsync();                        
            var mockGrandPrixRepo = new MockGrandPrixRepository().MockGetById(mockGrandPrix);
            var mockDriverRepo = new MockDriverRepository();
            var mockCarRepo = new MockCarRepository();
            var mockGrandPrixLoggerRepo = new MockLoggerRepository<GrandPrixesService>();
            var mockGrandsPrixService = new GrandPrixesService(mockGrandPrixRepo.Object, mockDriverRepo.Object, mockCarRepo.Object, mockGrandPrixLoggerRepo.Object);
            
            // Act
            var GrandPrix = await mockGrandsPrixService.GetGrandPrix(mockId);        

            // assert
            foreach (var race in GrandPrix)
            {
                Assert.Equal(mockId, race.raceid);
            }            
            mockGrandPrixRepo.VerifyGetByIdForGrandPrix(Times.Once());
        }        

        [Fact]
        public async void GrandsPrixService_GetById_NoGrandPrixs()
        {
            // Arrange
            var mockId = 2;
            var mockGrandPrix = MockGrandPrixData.GetEmptyEntityListAsync();                        
            var mockGrandPrixRepo = new MockGrandPrixRepository().MockGetById(mockGrandPrix);
            var mockDriverRepo = new MockDriverRepository();
            var mockCarRepo = new MockCarRepository();
            var mockGrandPrixLoggerRepo = new MockLoggerRepository<GrandPrixesService>();
            var mockGrandsPrixService = new GrandPrixesService(mockGrandPrixRepo.Object, mockDriverRepo.Object, mockCarRepo.Object, mockGrandPrixLoggerRepo.Object);

            // Act
            var GrandPrix = await mockGrandsPrixService.GetGrandPrix(mockId);        

            // Assert
            Assert.Null(GrandPrix);
            mockGrandPrixRepo.VerifyGetByIdForGrandPrix(Times.Once());
        }     
    }
}