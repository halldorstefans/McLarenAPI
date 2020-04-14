using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Entities;
using McLaren.Core.Models;

namespace McLaren.UnitTests.Mocks.Data
{
    public class MockDriverData
    {        
        public static async Task<IEnumerable<DriverDto>> GetAllModelListAsync()
        {
            return await Task.Run(() => GetAllModelList());
        }

        public static async Task<IEnumerable<DriverDto>> GetEmptyModelListAsync()
        {
            return await Task.Run(() => GetEmptyModelList());
        }

        public static async Task<DriverDto> GetSingleModelAsync()
        {
            return await Task.Run(() => GetSingleModel());
        }
        
        public static async Task<DriverDto> GetSingleEmptyModelAsync()
        {
            return await Task.Run(() => GetSingleEmptyModel());
        }
        private static DriverDto GetEmptySingleDTO()
        {            
            return null;            
        }
        private static IEnumerable<DriverDto> GetAllModelList()
        {
            return new List<DriverDto>()
            {
                new DriverDto()
                {
                    id = 13,
                    firstName = "Niki",
                    lastName = "Lauda",                
                    gpEntries = 100,
                    gpWins = 60,
                    gpPoles = 70,
                    gpFastestLap = 30,
                    gpPodiums = 50,
                    gpPoints = 500
                }
            };
        } 
        private static IEnumerable<DriverDto> GetEmptyModelList()
        {
            return new List<DriverDto>();
        } 
        private static DriverDto GetSingleModel()
        {            
            return new DriverDto()
            {
                id = 13,
                firstName = "Niki",
                lastName = "Lauda",                
                gpEntries = 100,
                gpWins = 60,
                gpPoles = 70,
                gpFastestLap = 30,
                gpPodiums = 50,
                gpPoints = 500
            };            
        } 

        private static DriverDto GetSingleEmptyModel()
        {            
            return null;
        } 

        public static async Task<IEnumerable<Driver>> GetAllEntitiesListAsync()
        {
            return await Task.Run(() => GetAllEntitiesList());
        }

        public static async Task<IEnumerable<Driver>> GetEmptyEntityListAsync()
        {
            return await Task.Run(() => GetEmptyEntityList());
        }

        public static async Task<Driver> GetSingleEntityAsync()
        {
            return await Task.Run(() => GetSingleEntity());
        }

        public static async Task<Driver> GetSingleEmptyEntityAsync()
        {
            return await Task.Run(() => GetSingleEmptyEntity());
        }
        private static IEnumerable<Driver> GetAllEntitiesList()
        {
            return new List<Driver>()
            {
                new Driver()
                {
                    id = 13,
                    firstName = "Niki",
                    lastName = "Lauda",                
                    gpEntries = 100,
                    gpWins = 60,
                    gpPoles = 70,
                    gpFastestLap = 30,
                    gpPodiums = 50,
                    gpPoints = 500
                }
            };
        } 
        private static IEnumerable<Driver> GetEmptyEntityList()
        {
            return new List<Driver>();
        } 
        private static Driver GetSingleEntity()
        {            
            return new Driver()
            {
                id = 13,
                firstName = "Niki",
                lastName = "Lauda",                
                gpEntries = 100,
                gpWins = 60,
                gpPoles = 70,
                gpFastestLap = 30,
                gpPodiums = 50,
                gpPoints = 500
            };            
        }     
        
        private static Driver GetSingleEmptyEntity()
        {            
            return null;            
        } 
    }
}