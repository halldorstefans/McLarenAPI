using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using McLaren.Core.Interfaces;
using McLaren.Core.Interfaces.Repositories;
using McLaren.Core.Models;
using McLaren.Core.Constants;
using McLaren.Core.ResourceParameters;
using McLaren.Core.Entities;

namespace McLaren.Core.Services
{
    public class CarsService : ICarsService
    {
        private readonly ICarsRepository _carsRepository;
        private readonly ILogger _logger;

        public CarsService(ICarsRepository carsRepository, ILogger<CarsService> logger)
        {
            _carsRepository = carsRepository;
            _logger = logger;
        }        

        public async Task<CarDto> GetCar(int id)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.GetItem, "Get Car by Id", id);

                var car = await _carsRepository.Get(id);

                if (car == null)
                {
                    _logger.LogInformation(LoggingEvents.GetItem, $"Could not find Car with id: { id }", id);
                    return null;
                }

                return car.Map();
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItem, ex, ex.Message, id);
                throw;
            }
        }

        public async Task<IEnumerable<CarDto>> GetCars()
        {
            try
            {
                _logger.LogInformation(LoggingEvents.ListItems, "Get all Cars", null);

                var cars = await _carsRepository.GetAll();

                if (cars.Count() == 0)
                {
                    _logger.LogInformation(LoggingEvents.GetItem, "No Cars found", null);
                    return Enumerable.Empty<CarDto>();
                }
                
                return cars.Select(c => c.Map());                        
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ListItems, ex, ex.Message, null);
                throw;
            }
        }

        public async Task<IEnumerable<CarDto>> GetCars(CarsResourceParameters carsResourceParameters)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.ListItems, "Get all Cars with filter", null);

                if (carsResourceParameters == null)
                {
                    throw new ArgumentNullException(nameof(carsResourceParameters));
                }

                if (string.IsNullOrWhiteSpace(carsResourceParameters.Name)
                    && string.IsNullOrWhiteSpace(carsResourceParameters.Year))
                {
                    return await GetCars();
                }                

                IEnumerable<Car> cars = Enumerable.Empty<Car>();

                if (!string.IsNullOrWhiteSpace(carsResourceParameters.Name))
                {
                    _logger.LogInformation(LoggingEvents.ListItems, "Get all Cars with name filter", null);

                    var nameFilter = carsResourceParameters.Name.Trim();
                    cars = await _carsRepository.GetByName(nameFilter.ToUpper());
                }

                if (!string.IsNullOrWhiteSpace(carsResourceParameters.Year))
                {
                    _logger.LogInformation(LoggingEvents.ListItems, "Get all Cars with year filter", null);

                    var yearFilter = Int32.Parse(carsResourceParameters.Year.Trim());
                    if (cars.Count() == 0)
                    {
                        _logger.LogInformation(LoggingEvents.GetItem, "No Cars found with year filter", null);
                        cars = await _carsRepository.GetByYear(yearFilter);
                    }
                    else
                    {
                        _logger.LogInformation(LoggingEvents.GetItem, "Found Cars found with year filter", null);
                        cars = cars.Where(c => c.fromyear <= yearFilter && c.toyear >= yearFilter);
                    }
                }

                if (cars.Count() == 0)
                {                
                    _logger.LogInformation(LoggingEvents.GetItem, "No Cars found", null);
                    return Enumerable.Empty<CarDto>();
                }
                
                return cars.Select(c => c.Map());                        
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ListItems, ex, ex.Message, null);
                throw;
            }
        }
    }    
}