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
    public class DriversController :  ControllerBase
    {
        private readonly IDriversService _driversService;

        public DriversController(IDriversService driversService)
        {
            _driversService = driversService;
        }

        /// <summary>
        /// Lists all drivers
        /// </summary>
        /// <returns>A list of all the drivers</returns>
        /// <response code="200">Returns the list of all drivers</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<DriverDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] DriversResourceParameters driversResourceParameters)
        {
            try
            {
                var drivers = await _driversService.GetDrivers(driversResourceParameters);

                return Ok(drivers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Driver with specified id
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns>A driver with specified id</returns>
        /// <response code="200">Returns the driver with the specified id</response>
        /// <response code="404">If no driver was found with the specified id</response>
        [HttpGet("{driverId:int}")]
        [ProducesResponseType(typeof(DriverDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> Get(int driverId)
        {
            try
            {                
                var driver = await _driversService.GetDriver(driverId);
                
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