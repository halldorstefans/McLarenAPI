using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using McLaren.Core.Interfaces;
using McLaren.Core.Interfaces.Repositories;
using McLaren.Core.Models;
using McLaren.Core.Constants;

namespace McLaren.Core.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger _logger;

        public CarService(ICarRepository carRepository, ILogger<CarService> logger)
        {
            _carRepository = carRepository;
            _logger = logger;
        }        

        public async Task<CarDto> GetById(int id)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.GetItem, "Get Car by Id", id);

                var car = await _carRepository.Get(id);

                if (car.id > 0)
                {
                    return car.Map();
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItem, ex, ex.Message, id);
                throw;
            }
        }

        public async Task<CarDto> GetByName(string name)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.GetItem, "Get Car by Name", name);

                var car = await _carRepository.Find(c => c.name == name);

                if (car.Count() > 0)
                {
                    return car.First().Map();
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItem, ex, ex.Message, name);
                throw;
            }
        }

        public async Task<IEnumerable<CarDto>> GetByYear(int year)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.GetItem, "Get Car by Year", year);

                var car = await _carRepository.GetByYear(year);                

                if (car.Count() > 0)
                {
                    return car.Select(c => c.Map());
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItem, ex, ex.Message, year);
                throw;
            }
        }

        public async Task<IEnumerable<CarDto>> GetAll()
        {
            try
            {
                _logger.LogInformation(LoggingEvents.ListItems, "Get all Cars", null);

                var cars = await _carRepository.GetAll();

                if (cars.Count() > 0)
                {                
                    return cars.Select(c => c.Map());
                }
                
                return null;                            
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ListItems, ex, ex.Message, null);
                throw;
            }
        }
    }
    
}