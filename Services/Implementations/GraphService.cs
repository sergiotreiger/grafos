using System.Threading.Tasks;
using Grafos.Services.Interfaces;
using Grafos.Models;
using Grafos.Repository.Interfaces;

namespace Grafos.Services.Implementations
{
    public class GraphService: IGraphService
    {
        private readonly IGraphRepository _graphRepository;

        public GraphService(IGraphRepository graphRepository)
        {
             _graphRepository = graphRepository;
        }
        public async Task<int> SaveGraph (Graph graph)
        {
           int idGrafo = await _graphRepository.SaveGraph(graph);
          return idGrafo;
        }

        public async Task<Graph> LoadGraph (int ID)
        {
            var graph = await _graphRepository.LoadGraph(ID);
            return graph;
        }
        
    }
}