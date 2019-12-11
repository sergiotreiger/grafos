using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Grafos.Repository.Interfaces;
using Grafos.Repository.Configuration;
using Grafos.Models;

namespace Grafos.Repository.Implementations
{
    public class GraphRepository: IGraphRepository
    {
        private readonly GraphDataContext _context;

        public GraphRepository(GraphDataContext context)
        {
             _context = context;
        }
        public async Task<int> SaveGraph (Graph graph)
        {
            int newGraphID=0;
            _context.Add(graph);

            int entitiesSaved = await _context.SaveChangesAsync();
            if (entitiesSaved > 0)
            {
                newGraphID = graph.ID;
            }
            return newGraphID;
        }

        public async Task<Graph> LoadGraph (int ID)
        {
           var graph = await _context.Graph.Include(p => p.Data).FirstOrDefaultAsync(g=>g.ID==ID);
           return graph;
        }
        
    }
}