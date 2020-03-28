using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Entities;
using McLaren.Core.Models;

namespace McLaren.UnitTests.Mocks.Data
{
    public class MockCarData
    {
        public static async Task<CarDto> GetEmptySingleDTOAsync()
        {
            return await Task.Run(() => GetEmptySingleDTO());
        }
        private static CarDto GetEmptySingleDTO()
        {            
            return null;            
        }
        public static async Task<CarDto> GetSingleDTOAsync()
        {
            return await Task.Run(() => GetSingleDTO());
        }
        private static CarDto GetSingleDTO()
        {            
            return new CarDto()
            {
                id = 2,
                name = "MCL35",
                fromYear = 2020,
                toYear = 2020
            };            
        } 

        public static async Task<IEnumerable<CarDto>> GetListDTOAsync()
        {
            return await Task.Run(() => GetListDTO());
        }

        public static async Task<IEnumerable<CarDto>> GetEmptyListDTOAsync()
        {
            return await Task.Run(() => GetEmptyListDTO());
        }
        private static IEnumerable<CarDto> GetListDTO()
        {
            return new List<CarDto>()
            {
                new CarDto()
                {
                    id = 2,
                    name = "MCL35",
                    fromYear = 2020,
                    toYear = 2020
                }
            };
        } 
        private static IEnumerable<CarDto> GetEmptyListDTO()
        {
            return new List<CarDto>()
            {
            };
        } 
        public static async Task<IEnumerable<Car>> GetListAsync()
        {
            return await Task.Run(() => GetList());
        }

        public static async Task<IEnumerable<Car>> GetEmptyListAsync()
        {
            return await Task.Run(() => GetEmptyList());
        }
        private static IEnumerable<Car> GetList()
        {
            return new List<Car>()
            {
                new Car()
                {
                    id = 2,
                    name = "MCL35",
                    fromyear = 2020,
                    toyear = 2020
                }
            };
        } 
        private static IEnumerable<Car> GetEmptyList()
        {
            return new List<Car>()
            {
            };
        } 
    }
}