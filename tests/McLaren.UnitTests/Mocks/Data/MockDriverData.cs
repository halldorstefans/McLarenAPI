using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Entities;
using McLaren.Core.Models;

namespace McLaren.UnitTests.Mocks.Data
{
    public class MockDriverData
    {
        public static async Task<DriverDto> GetEmptySingleDTOAsync()
        {
            return await Task.Run(() => GetEmptySingleDTO());
        }
        private static DriverDto GetEmptySingleDTO()
        {            
            return null;            
        }
        public static async Task<DriverDto> GetSingleDTOAsync()
        {
            return await Task.Run(() => GetSingleDTO());
        }
        private static DriverDto GetSingleDTO()
        {            
            return new DriverDto()
            {
                id = 2,
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

        public static async Task<IEnumerable<DriverDto>> GetListDTOAsync()
        {
            return await Task.Run(() => GetListDTO());
        }

        public static async Task<IEnumerable<DriverDto>> GetEmptyListDTOAsync()
        {
            return await Task.Run(() => GetEmptyListDTO());
        }
        private static IEnumerable<DriverDto> GetListDTO()
        {
            return new List<DriverDto>()
            {
                new DriverDto()
                {
                    id = 2,
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
        private static IEnumerable<DriverDto> GetEmptyListDTO()
        {
            return new List<DriverDto>()
            {
            };
        } 

        public static async Task<Driver> GetSingleAsync()
        {
            return await Task.Run(() => GetSingle());
        }
        private static Driver GetSingle()
        {            
            return new Driver()
            {
                id = 2,
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
        public static async Task<IEnumerable<Driver>> GetListAsync()
        {
            return await Task.Run(() => GetList());
        }        
        private static IEnumerable<Driver> GetList()
        {
            return new List<Driver>()
            {
                new Driver()
                {
                    id = 2,
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
        public static async Task<IEnumerable<Driver>> GetEmptyListAsync()
        {
            return await Task.Run(() => GetEmptyList());
        }
        private static IEnumerable<Driver> GetEmptyList()
        {
            return new List<Driver>()
            {
            };
        } 
    }
}