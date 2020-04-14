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
using System.Globalization;

namespace McLaren.Core.Services
{
    public class DriversService : IDriversService
    {
        private readonly IDriversRepository _driversRepository;
        private readonly ILogger _logger;

        public DriversService(IDriversRepository driversRepository, ILogger<DriversService> logger)
        {
            _driversRepository = driversRepository;
            _logger = logger;
        }       

        public async Task<DriverDto> GetDriver(int id)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.GetItem, "Get Driver by Id", id);

                var driver = await _driversRepository.Get(id);

                if (driver == null)
                {
                    return null;
                }

                return driver.Map();

            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItem, ex, ex.Message, id);
                throw;
            }
        }

        public async Task<IEnumerable<DriverDto>> GetDrivers()
        {
            try
            {
                _logger.LogInformation(LoggingEvents.ListItems, "Get all Drivers", null);

                var drivers = await _driversRepository.GetAll();

                if (drivers.Count() == 0)
                {
                    return Enumerable.Empty<DriverDto>();
                }

                return drivers.Select(d => d.Map());
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ListItems, ex, ex.Message, null);
                throw;
            }
        }

        public async Task<IEnumerable<DriverDto>> GetDrivers(DriversResourceParameters driversResourceParameters)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.ListItems, "Get all Drivers with filter", null);

                if (driversResourceParameters == null)
                {
                    throw new ArgumentNullException(nameof(driversResourceParameters));
                }

                if (string.IsNullOrWhiteSpace(driversResourceParameters.Name))
                {
                    return await GetDrivers();
                }                

                _logger.LogInformation(LoggingEvents.ListItems, "Get all Drivers with name filter", null);

                var nameFilter = driversResourceParameters.Name.Trim().ToLower();
                var drivers = await _driversRepository.GetByName(NameToTitleCase(nameFilter));
                
                if (drivers.Count() == 0)
                {
                    return Enumerable.Empty<DriverDto>();
                }

                return drivers.Select(d => d.Map());
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ListItems, ex, ex.Message, null);
                throw;
            }
        }

        private string NameToTitleCase(string name)
        {
            TextInfo textInfo = new CultureInfo("en-GB", false).TextInfo;

            if (name == "mclaren")
            {
                name = name.Replace("m", "M").Replace("l", "L");
            }
            else if (name.Contains(" "))
            {
                var splitNames = name.Split();
                List<string> newNames = new List<string>();

                foreach (var word in splitNames)
                {
                    var newName = word;
                    if (newName != "de" && newName != "van")
                    {                            
                        newName = char.ToUpper(newName[0]) + ((newName.Length > 1) ? newName.Substring(1).ToLower() : string.Empty);                     
                    }
                    newNames.Add(newName);
                }
                name = string.Join(" ", newNames);
            }
            else
            {
                name = textInfo.ToTitleCase(name);
            }

            return name;
        }
    }
    
}