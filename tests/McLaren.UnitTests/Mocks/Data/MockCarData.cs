using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Entities;
using McLaren.Core.Models;

namespace McLaren.UnitTests.Mocks.Data
{
    public class MockCarData
    {
        public static async Task<IEnumerable<CarDto>> GetAllModelListAsync()
        {
            return await Task.Run(() => GetAllModelList());
        }

        public static async Task<IEnumerable<CarDto>> GetEmptyModelListAsync()
        {
            return await Task.Run(() => GetEmptyModelList());
        }

        public static async Task<CarDto> GetSingleModelAsync()
        {
            return await Task.Run(() => GetSingleModel());
        }
        
        public static async Task<CarDto> GetSingleEmptyModelAsync()
        {
            return await Task.Run(() => GetSingleEmptyModel());
        }
        private static IEnumerable<CarDto> GetAllModelList()
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
        private static IEnumerable<CarDto> GetEmptyModelList()
        {
            return new List<CarDto>();
        } 

        private static CarDto GetSingleModel()
        {            
            return new CarDto()
            {
                id = 2,
                name = "MCL35",
                fromYear = 2020,
                toYear = 2020
            };            
        } 

        private static CarDto GetSingleEmptyModel()
        {            
            return null;            
        } 
        public static async Task<IEnumerable<Car>> GetAllEntitiesListAsync()
        {
            return await Task.Run(() => GetAllEntitiesList());
        }

        public static async Task<IEnumerable<Car>> GetEmptyEntityListAsync()
        {
            return await Task.Run(() => GetEmptyEntityList());
        }

        public static async Task<Car> GetSingleEntityAsync()
        {
            return await Task.Run(() => GetSingleEntity());
        }

        public static async Task<Car> GetSingleEmptyEntityAsync()
        {
            return await Task.Run(() => GetSingleEmptyEntity());
        }
        private static IEnumerable<Car> GetAllEntitiesList()
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
        private static IEnumerable<Car> GetEmptyEntityList()
        {
            return new List<Car>()
            {
            };
        } 
        
        private static Car GetSingleEntity()
        {            
            return new Car()
            {
                id = 2,
                name = "MCL35",
                fromyear = 2020,
                toyear = 2020
            };            
        } 

        private static Car GetSingleEmptyEntity()
        {            
            return null;
        } 
    }
}