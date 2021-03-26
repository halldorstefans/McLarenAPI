using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using McLaren.Core.Interfaces;
using McLaren.Core.Models;
using McLaren.Core.ResourceParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace McLaren.Web.V0_9.Controller
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/formula1/v{version:apiVersion}/[controller]")]    
    public class CarsController :  ControllerBase
    {
        private readonly ICarsService _carsService;
        private readonly ILogger<CarsController> _logger;

        public CarsController(ICarsService carsService, ILogger<CarsController> logger)
        {
            _carsService = carsService;
            _logger = logger;
        }

        /// <summary>
        /// Lists all cars
        /// </summary>
        /// <param name="carsResourceParameters"></param>
        /// <returns>A list of all the cars</returns>
        /// <response code="200">Returns the list of all cars</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<CarDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] CarsResourceParameters carsResourceParameters)
        {
            _logger.LogInformation("API ENTRY: Inside get all cars API call.");
            var cars = await _carsService.GetCars(carsResourceParameters);

            return Ok(cars);
        }

        /// <summary>
        /// Car with specified id
        /// </summary>
        
        /// <returns>A car with specified id</returns>
        /// <response code="200">Returns the car with the specified id</response>
        /// <response code="404">If no car was found with the specified id</response>
        [HttpGet("{carId:int}")]
        [ProducesResponseType(typeof(CarDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]  
        public async Task<IActionResult> Get(int carId)
        {
            _logger.LogInformation("API ENTRY: Inside get car by Id API call.");
            var car = await _carsService.GetCar(carId);

            if (car == null)
            {
                _logger.LogInformation("No car found in API call.");
                return NotFound();
            }

            return Ok(car);          
        }
    }
}