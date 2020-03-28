using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using McLaren.Core.Interfaces;
using McLaren.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace McLaren.Web.V1.Controller
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/formula1/v{version:apiVersion}/[controller]")]
    public class CarController :  ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        /// <summary>
        /// Lists all cars
        /// </summary>
        /// <returns>A list of all the cars</returns>
        /// <response code="200">Returns the list of all cars</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<CarDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cars = await _carService.GetAll();
                if (cars == null)
                {
                    return NotFound();
                }

                return Ok(cars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        /// <summary>
        /// Lists all cars in specified year
        /// </summary>
        /// <param name="year"></param>
        /// <returns>A list of all cars in specified year</returns>
        /// <response code="200">Returns the list of all cars in specified year</response>
        /// <response code="404">If no cars were found in the specified year</response>
        [HttpGet("{year:int}")]
        [ProducesResponseType(typeof(List<CarDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> Get(int year)
        {
            try
            {
                var cars = await _carService.GetByYear(year);
                if (cars == null)
                {
                    return NotFound();
                }

                return Ok(cars);
            }
            catch (Exception ex)
            {                
                return StatusCode(500, ex);
            }            
        }

        /// <summary>
        /// Car with specified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A car with specified name</returns>
        /// <response code="200">Returns the car with the specified name</response>
        /// <response code="404">If no car was found with the specified name</response>
        [HttpGet("{name:regex([[a-z]]{{1}})}")]
        [ProducesResponseType(typeof(CarDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> Get(string name)
        {
            try
            {        
                var car = await _carService.GetByName(name.ToUpper());

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