using System;
using System.Threading.Tasks;
using McLaren.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace McLaren.Web.Controller
{
    [Route("api/f1/[controller]")]
    [ApiController]
    public class CarController :  ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        [HttpGet("{year:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpGet("{name:regex([[a-z]]{{1}})}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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