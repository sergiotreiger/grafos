using Grafos.Models;
using System.Threading.Tasks;

namespace Grafos.Services.Interfaces
{
    public interface IGraphService
    {
        Task<int> SaveGraph (Graph graph);

        Task<Graph> LoadGraph (int ID);

    }
}