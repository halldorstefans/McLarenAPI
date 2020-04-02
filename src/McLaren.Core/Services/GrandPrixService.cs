using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using McLaren.Core.Interfaces;
using McLaren.Core.Interfaces.Repositories;
using McLaren.Core.Models;
using McLaren.Core.Constants;
using McLaren.Core.Entities;

namespace McLaren.Core.Services
{
    public class GrandPrixService : IGrandPrixService
    {
        private readonly IGrandPrixRepository _grandPrixRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly ICarRepository _carRepository;
        private readonly ILogger _logger;

        public GrandPrixService(IGrandPrixRepository grandPrixRepository, IDriverRepository driverRepository, ICarRepository carRepository, ILogger<GrandPrixService> logger)
        {
            _grandPrixRepository = grandPrixRepository;
            _driverRepository = driverRepository;
            _carRepository = carRepository;
            _logger = logger;
        }        

        public async Task<GrandPrixDto> GetById(int id)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.GetItem, "Get Grand Prix by Id", id);

                var grandPrix = await _grandPrixRepository.Get(id);

                if (grandPrix == null)
                {
                    return null;
                }                
                
                var grandPrixDrivers = await _grandPrixRepository.Find(gp => gp.raceid == grandPrix.raceid);

                var grandPrixDto = grandPrix.Map();

                if (grandPrixDrivers.Count() > 0)
                {
                    grandPrixDto.drivers = grandPrixDrivers.Select(gpd => gpd.MapDriver());
                }

                return grandPrixDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItem, ex, ex.Message, id);
                throw;
            }
        }

        public async Task<IEnumerable<GrandPrixDto>> GetByYear(int year)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.GetItem, "Get Grands Prix by Year", year);

                var grandsPrix = await _grandPrixRepository.Find(gp => gp.year == year);                

                if (grandsPrix.Count() == 0)
                {
                    return Enumerable.Empty<GrandPrixDto>();
                }

                return await GetGrandPrixDrivers(grandsPrix);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItem, ex, ex.Message, year);
                throw;
            }
        }

        public async Task<IEnumerable<GrandPrixDto>> GetAll()
        {
            try
            {
                _logger.LogInformation(LoggingEvents.ListItems, "Get all Grands Prix", null);

                var grandsPrix = await _grandPrixRepository.GetAll();

                if (grandsPrix.Count() == 0)
                {
                    return Enumerable.Empty<GrandPrixDto>();
                }

                return await GetGrandPrixDrivers(grandsPrix);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ListItems, ex, ex.Message, null);
                throw;
            }
        }

        private async Task<IEnumerable<GrandPrixDto>> GetGrandPrixDrivers(IEnumerable<GrandPrix> grandsPrix)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.ListItems, "Get Grand Prix Drivers", null);

                var grandPrixDto = grandsPrix.Select(gp => gp.Map());

                var grandPrixList = grandPrixDto.GroupBy(gp => gp.raceid).Select(g => g.First()).ToList();

                foreach (var race in grandPrixList)
                {
                    var grandPrixDrivers = await _grandPrixRepository.Find(gp => gp.raceid == race.raceid);
                    race.drivers = grandPrixDrivers.Select(gpd => gpd.MapDriver());              
                }

                return grandPrixList;
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ListItems, ex, ex.Message, null);
                throw;
            }
        }
    }
    
}