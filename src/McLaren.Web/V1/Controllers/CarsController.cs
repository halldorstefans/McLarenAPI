using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using McLaren.Core.Interfaces;
using McLaren.Core.Models;
using McLaren.Core.ResourceParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace McLaren.Web.V1.Controller
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/formula1/v{version:apiVersion}/[controller]")]
    public class CarsController :  ControllerBase
    {
        private readonly ICarsService _carsService;

        public CarsController(ICarsService carsService)
        {
            _carsService = carsService;
        }

        /// <summary>
        /// Lists all cars
        /// </summary>
        /// <returns>A list of all the cars</returns>
        /// <response code="200">Returns the list of all cars</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<CarDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] CarsResourceParameters carsResourceParameters)
        {
            try
            {
                var cars = await _carsService.GetCars(carsResourceParameters);

                return Ok(cars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Car with specified id
        /// </summary>
        /// <param name="carId"></param>
        /// <returns>A car with specified id</returns>
        /// <response code="200">Returns the car with the specified id</response>
        /// <response code="404">If no car was found with the specified id</response>
        [HttpGet("{carId:int}")]
        [ProducesResponseType(typeof(CarDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]  
        public async Task<IActionResult> Get(int carId)
        {
            try
            {
                var car = await _carsService.GetCar(carId);

                if (car == null)
                {
                    return NotFound();
                }

                return Ok(car);
            }
            catch (Exception ex)
            {                
                return StatusCode(500, ex);
            }            
        }
    }
}