using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Grafos.Models;
using Grafos.Services.Interfaces;

namespace Grafos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DistanceController : ControllerBase
    {
        private readonly IRouteService _routeService;
        private readonly ILogger _logger;

        public DistanceController (IRouteService routeService, ILogger<DistanceController> logger)
        {
            _routeService = routeService;
            _logger = logger;
        }

      

        [HttpPost("{graphID}/from/{town1}/to/{town2}")]
        public async Task<ActionResult> PostAsync(int graphID, string town1, string town2)
        {
             try
            {
                DistanceBetweenCities minDistance = await _routeService.FindMinimumDistanceByGraphID(graphID, town1, town2 );
                if (minDistance.Path.Count > 0 )
                    {
                        return Ok(minDistance);
                    }
                else
                    {
                        return NotFound();
                    }
            }
            catch (Exception erroProcessamento)
            {
                _logger.LogError(erroProcessamento,"");
                return BadRequest();
            }
        }

       
    }
}
