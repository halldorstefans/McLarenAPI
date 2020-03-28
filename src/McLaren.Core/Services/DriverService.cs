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

                if (driver != null)
                {
                    return driver.Map();
                }

                return null;

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

                lastName = NameToTitleCase(lastName);

                var driver = await _driverRepository.Find(d => d.lastName == lastName);

                if (driver.Count() > 0)
                {
                    return driver.Select(d => d.Map());
                }

                return null;
              
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

                if (drivers.Count() > 0)
                {
                    return drivers.Select(d => d.Map());
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ListItems, ex, ex.Message, null);
                throw;
            }
        }

        private string NameToTitleCase(string lastName)
        {
            if (lastName == "mclaren")
            {
                lastName = lastName.Replace("m", "M").Replace("l", "L");
            }

            if (lastName.Contains("_"))
            {
                lastName = lastName.Replace("_", " ");
                var splitLastNames = lastName.Split();
                List<string> newLastNames = new List<string>();

                foreach (var name in splitLastNames)
                {
                    var newname = name;
                    if (newname != "de" && newname != "van")
                    {                            
                        newname = char.ToUpper(newname[0]) + ((newname.Length > 1) ? newname.Substring(1).ToLower() : string.Empty);                     
                    }
                    newLastNames.Add(newname);
                }
                lastName = string.Join(" ", newLastNames);
            }

            return lastName;
        }
    }
    
}