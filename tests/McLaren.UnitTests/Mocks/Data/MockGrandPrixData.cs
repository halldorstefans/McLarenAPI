using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Entities;
using McLaren.Core.Models;

namespace McLaren.UnitTests.Mocks.Data
{
    public class MockGrandPrixData
    {
        public static async Task<IEnumerable<GrandPrixDto>> GetAllModelListAsync()
        {
            return await Task.Run(() => GetAllModelList());
        }

        public static async Task<IEnumerable<GrandPrixDto>> GetEmptyModelListAsync()
        {
            return await Task.Run(() => GetEmptyModelList());
        }

        public static async Task<IEnumerable<GrandPrixDto>> GetNullModelListAsync()
        {
            return await Task.Run(() => GetNullModelList());
        }

        public static async Task<GrandPrixDto> GetSingleModelAsync()
        {
            return await Task.Run(() => GetSingleModel());
        }
        
        public static async Task<GrandPrixDto> GetSingleEmptyModelAsync()
        {
            return await Task.Run(() => GetSingleEmptyModel());
        }
        
        private static GrandPrixDto GetEmptySingleDTO()
        {            
            return null;            
        }

        private static IEnumerable<GrandPrixDto> GetAllModelList()
        {
            return new List<GrandPrixDto>()
            {
                new GrandPrixDto()
                {
                    raceid = 15,
                    year = 1970,
                    country = "Spain",
                    drivers = new []
                    {
                        new GrandPrixDriverDto{
                            team = "McLaren",
                            carNumber = 5,                
                            driver = "Niki Lauda",
                            car = "M7B",
                            engine = "Mercedes",
                            tyre = "tyre",
                            grid = "3",
                            position = "1",
                            comment = "comment"
                        }
                    }                
                }
            };
        } 
        private static IEnumerable<GrandPrixDto> GetEmptyModelList()
        {
            return new List<GrandPrixDto>();
        } 

        private static IEnumerable<GrandPrixDto> GetNullModelList()
        {
            return null;
        } 
        private static GrandPrixDto GetSingleModel()
        {            
            return new GrandPrixDto()
            {
                raceid = 15,
                year = 1970,
                country = "Spain",
                drivers = new []
                {
                    new GrandPrixDriverDto{
                        team = "McLaren",
                        carNumber = 5,                
                        driver = "Niki Lauda",
                        car = "M7B",
                        engine = "Mercedes",
                        tyre = "tyre",
                        grid = "3",
                        position = "1",
                        comment = "comment"
                    }
                }  
            };            
        } 

        private static GrandPrixDto GetSingleEmptyModel()
        {            
            return null;            
        } 

        public static async Task<IEnumerable<GrandPrix>> GetAllEntitiesListAsync()
        {
            return await Task.Run(() => GetAllEntitiesList());
        }

        public static async Task<IEnumerable<GrandPrix>> GetEmptyEntityListAsync()
        {
            return await Task.Run(() => GetEmptyEntityList());
        }

        public static async Task<GrandPrix> GetSingleEntityAsync()
        {
            return await Task.Run(() => GetSingleEntity());
        }

        public static async Task<GrandPrix> GetSingleEmptyEntityAsync()
        {
            return await Task.Run(() => GetSingleEmptyEntity());
        }
        private static IEnumerable<GrandPrix> GetAllEntitiesList()
        {
            return new List<GrandPrix>()
            {
                new GrandPrix()
                {
                    id = 2,
                    raceid = 15,
                    year = 1970,
                    country = "Spain",
                    team = "McLaren",
                    carNumber = 5,                
                    driverId = 13,
                    carId = 2,
                    engine = "Mercedes",
                    tyre = "tyre",
                    grid = "3",
                    position = "1",
                    comment = "comment"
                }
            };
        } 
        private static IEnumerable<GrandPrix> GetEmptyEntityList()
        {
            return new List<GrandPrix>();
        } 
        private static GrandPrix GetSingleEntity()
        {            
            return new GrandPrix()
            {
                id = 2,
                raceid = 15,
                year = 1970,
                country = "Spain",
                team = "McLaren",
                carNumber = 5,                
                driverId = 13,
                carId = 7,
                engine = "Mercedes",
                tyre = "tyre",
                grid = "3",
                position = "1",
                comment = "comment"
            };            
        }     

        private static GrandPrix GetSingleEmptyEntity()
        {            
            return null;            
        } 
    }
}