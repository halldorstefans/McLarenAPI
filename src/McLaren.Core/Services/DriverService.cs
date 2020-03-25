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
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly ILogger _logger;

        public DriverService(IDriverRepository driverRepository, ILogger<DriverService> logger)
        {
            _driverRepository = driverRepository;
            _logger = logger;
        }       

        public async Task<DriverDto> GetById(int id)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.GetItem, "Get Driver by Id", id);

                var driver = await _driverRepository.Get(id);

                var driverDto = driver.Map();

                return driverDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItem, ex, ex.Message, id);
                throw;
            }
        }

        public async Task<IEnumerable<DriverDto>> GetByLastName(string lastName)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.GetItem, "Get Driver by LastName", lastName);

                if (lastName.Contains("_"))
                {
                    lastName = lastName.Replace("_", " ");
                    Console.WriteLine(lastName);
                }

                var driver = await _driverRepository.Find(d => d.lastName == lastName);

                var driverDto = driver.Select(d => d.Map());

                return driverDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItem, ex, ex.Message, lastName);
                throw;
            }
        }

        public async Task<IEnumerable<DriverDto>> GetAll()
        {
            try
            {
                _logger.LogInformation(LoggingEvents.ListItems, "Get all Drivers", null);

                var drivers = await _driverRepository.GetAll();

                var driversDto = drivers.Select(d => d.Map());

                return driversDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ListItems, ex, ex.Message, null);
                throw;
            }
        }
    }
    
}