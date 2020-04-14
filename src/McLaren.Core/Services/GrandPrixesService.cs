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
using McLaren.Core.ResourceParameters;
using System.Globalization;

namespace McLaren.Core.Services
{
    public class GrandPrixesService : IGrandPrixesService
    {
        private readonly IGrandPrixesRepository _grandPrixesRepository;
        private readonly IDriversRepository _driversRepository;
        private readonly ICarsRepository _carsRepository;
        private readonly ILogger _logger;

        public GrandPrixesService(IGrandPrixesRepository grandPrixesRepository, IDriversRepository driversRepository, ICarsRepository carsRepository, ILogger<GrandPrixesService> logger)
        {
            _grandPrixesRepository = grandPrixesRepository;
            _driversRepository = driversRepository;
            _carsRepository = carsRepository;
            _logger = logger;
        }        

        public async Task<IEnumerable<GrandPrixDto>> GetGrandPrix(int raceId)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.GetItem, "Get Grand Prix by raceId", raceId);

                var grandPrixes = await _grandPrixesRepository.GetByRaceId(raceId);

                if (grandPrixes.Count() == 0)
                {
                    return null;
                }               
                
                return await GetGrandPrixDrivers(grandPrixes);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItem, ex, ex.Message, raceId);
                throw;
            }
        }

        public async Task<IEnumerable<GrandPrixDto>> GetGrandPrixes()
        {
            try
            {
                _logger.LogInformation(LoggingEvents.ListItems, "Get all Grands Prix", null);

                var grandPrixes = await _grandPrixesRepository.GetAll();

                if (grandPrixes.Count() == 0)
                {
                    return null;
                }

                return await GetGrandPrixDrivers(grandPrixes);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.ListItems, ex, ex.Message, null);
                throw;
            }
        }

        public async Task<IEnumerable<GrandPrixDto>> GetGrandPrixes(GrandPrixesResourceParameters grandPrixesResourceParameters)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.ListItems, "Get all Grands Prix with filter", null);

                if (grandPrixesResourceParameters == null)
                {
                    throw new ArgumentNullException(nameof(grandPrixesResourceParameters));
                }

                if (string.IsNullOrWhiteSpace(grandPrixesResourceParameters.Country)
                    && string.IsNullOrWhiteSpace(grandPrixesResourceParameters.Year))
                {
                    return await GetGrandPrixes();
                }

                IEnumerable<GrandPrix> grandPrixes = Enumerable.Empty<GrandPrix>();
                TextInfo textInfo = new CultureInfo("en-GB", false).TextInfo;

                if (!string.IsNullOrWhiteSpace(grandPrixesResourceParameters.Country))
                {
                    _logger.LogInformation(LoggingEvents.ListItems, "Get all Grands Prix with country filter", null);

                    var countryFilter = grandPrixesResourceParameters.Country.Trim();
                    grandPrixes = await _grandPrixesRepository.GetByCountry(textInfo.ToTitleCase(countryFilter));
                }

                if (!string.IsNullOrWhiteSpace(grandPrixesResourceParameters.Year))
                {
                    _logger.LogInformation(LoggingEvents.ListItems, "Get all Grands Prix with year filter", null);

                    var yearFilter = grandPrixesResourceParameters.Year.Trim();
                    grandPrixes = await _grandPrixesRepository.GetByYear(Int32.Parse(yearFilter));
                }

                if (grandPrixes.Count() == 0)
                {
                    return Enumerable.Empty<GrandPrixDto>();
                }

                return await GetGrandPrixDrivers(grandPrixes);
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
                    var grandPrixDrivers = await _grandPrixesRepository.Find(gp => gp.raceid == race.raceid);
                    race.drivers = grandPrixDrivers.Select(gpd => gpd.MapDriver(_carsRepository, _driversRepository)).ToList();
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