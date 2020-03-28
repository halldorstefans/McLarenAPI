using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Entities;
using McLaren.Core.Models;

namespace McLaren.UnitTests.Mocks.Data
{
    public class MockGrandPrixData
    {
        public static async Task<GrandPrixDto> GetEmptySingleDTOAsync()
        {
            return await Task.Run(() => GetEmptySingleDTO());
        }
        private static GrandPrixDto GetEmptySingleDTO()
        {            
            return null;            
        }
        public static async Task<GrandPrixDto> GetSingleDTOAsync()
        {
            return await Task.Run(() => GetSingleDTO());
        }
        private static GrandPrixDto GetSingleDTO()
        {            
            return new GrandPrixDto()
            {
                id = 2,
                raceid = 15,
                year = 1970,
                country = "Spain"
            };            
        } 

        public static async Task<IEnumerable<GrandPrixDto>> GetListDTOAsync()
        {
            return await Task.Run(() => GetListDTO());
        }

        public static async Task<IEnumerable<GrandPrixDto>> GetEmptyListDTOAsync()
        {
            return await Task.Run(() => GetEmptyListDTO());
        }
        private static IEnumerable<GrandPrixDto> GetListDTO()
        {
            return new List<GrandPrixDto>()
            {
                new GrandPrixDto()
                {
                    id = 2,
                    raceid = 15,
                    year = 1970,
                    country = "Spain"
                }
            };
        } 
        private static IEnumerable<GrandPrixDto> GetEmptyListDTO()
        {
            return new List<GrandPrixDto>()
            {
            };
        } 

        public static async Task<GrandPrix> GetSingleAsync()
        {
            return await Task.Run(() => GetSingle());
        }
        private static GrandPrix GetSingle()
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
        public static async Task<IEnumerable<GrandPrix>> GetListAsync()
        {
            return await Task.Run(() => GetList());
        }        
        private static IEnumerable<GrandPrix> GetList()
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
                    carId = 7,
                    engine = "Mercedes",
                    tyre = "tyre",
                    grid = "3",
                    position = "1",
                    comment = "comment"
                }
            };
        } 
        public static async Task<IEnumerable<GrandPrix>> GetEmptyListAsync()
        {
            return await Task.Run(() => GetEmptyList());
        }
        private static IEnumerable<GrandPrix> GetEmptyList()
        {
            return new List<GrandPrix>()
            {
            };
        } 
    }
}