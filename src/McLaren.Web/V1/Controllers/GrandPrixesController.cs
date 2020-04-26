using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using McLaren.Core.Interfaces;
using McLaren.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using McLaren.Core.ResourceParameters;

namespace McLaren.Web.V0_9.Controller
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/formula1/v{version:apiVersion}/[controller]")]
    public class GrandPrixesController :  ControllerBase
    {
        private readonly IGrandPrixesService _grandPrixesService;

        public GrandPrixesController(IGrandPrixesService grandPrixesService)
        {
            _grandPrixesService = grandPrixesService;
        }

        /// <summary>
        /// Lists all Grands Prix
        /// </summary>
        /// <returns>A list of all Grands Prix</returns>
        /// <response code="200">Returns the list of all Grands Prix</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<GrandPrixDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GrandPrixesResourceParameters grandPrixesResourceParameters)
        {
            var grandPrixes = await _grandPrixesService.GetGrandPrixes(grandPrixesResourceParameters);

            return Ok(grandPrixes);            
        }

        /// <summary>
        /// Grand Prix with specified raceId
        /// </summary>
        /// <param name="raceId"></param>
        /// <returns>A Grand Prix with specified raceId</returns>
        /// <response code="200">Returns the Grand Prix with specified raceId</response>
        /// <response code="404">If no Grand Prix was found with the specified raceId</response>
        [HttpGet("{raceId:int}")]
        [ProducesResponseType(typeof(List<GrandPrixDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]        
        public async Task<IActionResult> Get(int raceId)
        {
            var grandPrix = await _grandPrixesService.GetGrandPrix(raceId);

            if (grandPrix == null)
            {
                return NotFound();
            }

            return Ok(grandPrix);          
        }
    }
}