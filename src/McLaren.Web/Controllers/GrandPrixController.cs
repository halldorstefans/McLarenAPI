using System;
using System.Threading.Tasks;
using McLaren.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace McLaren.Web.Controller
{
    [Route("api/f1/[controller]")]
    [ApiController]
    public class GrandPrixController :  ControllerBase
    {
        private readonly IGrandPrixService _grandPrixService;

        public GrandPrixController(IGrandPrixService grandPrixService)
        {
            _grandPrixService = grandPrixService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return new ObjectResult(await _grandPrixService.GetAll());
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
                var grandPrix = await _grandPrixService.GetByYear(year);

                if (grandPrix == null)
                {
                    return NotFound();
                }

                return Ok(grandPrix);
            }
            catch (Exception ex)
            {                
                return StatusCode(500, ex);
            }            
        }
    }
}