using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Grafos.Models;
using Grafos.Services.Interfaces;
using Microsoft.Extensions.Logging;


namespace Grafos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteService _routeService;
        private readonly ILogger _logger;

        public  RoutesController (IRouteService routeService , ILogger<RoutesController> logger)
        {
            _routeService = routeService;
            _logger = logger;
        }
    
        [HttpPost("{graphID}/from/{town1}/to/{town2}/maxStops/{maxStops}")]
        public async Task<ActionResult> PostAsync(int graphID, string town1, string town2, int maxStops)
        {
             try
            {
                RouteList routes = await _routeService.FindRoutesByGraphID(graphID, town1, town2, maxStops );
                if (routes.Routes.Count > 0 )
                    {
                        return Ok(routes);
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
