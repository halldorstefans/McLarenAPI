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
    public class DriverController :  ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        /// <summary>
        /// Lists all drivers
        /// </summary>
        /// <returns>A list of all the drivers</returns>
        /// <response code="200">Returns the list of all drivers</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<DriverDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var drivers = await _driverService.GetAll();

                if (drivers == null)
                {
                    return NotFound();
                }

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

        /// <summary>
        /// Lists all driver with specified last name
        /// </summary>
        /// <param name="lastname"></param>
        /// <returns>A list of all drivers with specified last name</returns>
        /// <response code="200">Returns the list of all drivers with specified last name</response>
        /// <response code="404">If no drivers were found with the specified last name</response>
        [HttpGet("{lastname:regex([[a-z]])}")]
        [ProducesResponseType(typeof(List<DriverDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> Get(string lastname)
        {
            try
            {
                var drivers = await _driverService.GetByLastName(lastname.ToLower());

                if (drivers == null)
                {
                    return NotFound();
                }

                return Ok(drivers);
            }
            catch (Exception ex)
            {                
                return StatusCode(500, ex);
            }            
        }
    }
}