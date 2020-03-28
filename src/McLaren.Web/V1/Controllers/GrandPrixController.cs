using System;
using System.Collections.Generic;
using System.Net;
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
    public class GrandPrixController :  ControllerBase
    {
        private readonly IGrandPrixService _grandPrixService;

        public GrandPrixController(IGrandPrixService grandPrixService)
        {
            _grandPrixService = grandPrixService;
        }

        /// <summary>
        /// Lists all Grands Prix
        /// </summary>
        /// <returns>A list of all Grands Prix</returns>
        /// <response code="200">Returns the list of all Grands Prix</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<GrandPrixDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var grandsPrix = await _grandPrixService.GetAll();

                if (grandsPrix == null)
                {
                    return NotFound();
                }

                return Ok(grandsPrix);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Lists all Grands Prix in specified year
        /// </summary>
        /// <param name="year"></param>
        /// <returns>A list of all Grands Prix in specified year</returns>
        /// <response code="200">Returns the list of all Grands Prix in specified year</response>
        /// <response code="404">If no Grands Prix were found in the specified year</response>
        [HttpGet("{year:int}")]
        [ProducesResponseType(typeof(List<GrandPrixDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]        
        public async Task<IActionResult> Get(int year)
        {
            try
            {
                var grandsPrix = await _grandPrixService.GetByYear(year);

                if (grandsPrix == null)
                {
                    return NotFound();
                }

                return Ok(grandsPrix);
            }
            catch (Exception ex)
            {                
                return StatusCode(500, ex);
            }            
        }
    }
}