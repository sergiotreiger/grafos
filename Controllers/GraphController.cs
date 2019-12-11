using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Grafos.Services.Interfaces;
using Grafos.Models;
using System;

namespace Grafos.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GraphController : ControllerBase
    {

        private readonly IGraphService _graphService;
        private readonly ILogger _logger;

        public GraphController (IGraphService graphService, ILogger<GraphController> logger)
        {
            _graphService = graphService;
            _logger = logger;
        }

        // recupera um grafo pelo ID
        [HttpGet("{id}")]
         public async Task<ActionResult> GetAsync(int id)
        {
            try {
                    _logger.LogInformation("Get {ID}");
                    Graph graph = await _graphService.LoadGraph(id);

                    if ( (graph != null) )
                    {
                        return Ok(graph);
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

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Graph graph)
        {
            try
            {
                int newGraphID = await _graphService.SaveGraph(graph);

                if (newGraphID > 0 )
                {
                        return CreatedAtAction("PostAsync",graph);
                }
                else
                {
                    return BadRequest();
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
