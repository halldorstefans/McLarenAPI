using System;
using System.Threading.Tasks;
using McLaren.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace McLaren.Web.Controller
{
    [Route("api/f1/[controller]")]
    [ApiController]
    public class DriverController :  ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return new ObjectResult(await _driverService.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{driverId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int driverId)
        {
            try
            {
                var driver = await _driverService.GetById(driverId);

                if (driver == null)
                {
                    return NotFound();
                }

                return Ok(driver);
            }
            catch (Exception ex)
            {                
                return StatusCode(500, ex);
            }            
        }

        [HttpGet("{lastname:regex([[a-z]])}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string lastname)
        {
            try
            {
                Console.WriteLine(lastname);
                var driver = await _driverService.GetByLastName(lastname);

                if (driver == null)
                {
                    return NotFound();
                }

                return Ok(driver);
            }
            catch (Exception ex)
            {                
                return StatusCode(500, ex);
            }            
        }
    }
}