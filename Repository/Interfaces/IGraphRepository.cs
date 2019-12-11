using Grafos.Models;
using System.Threading.Tasks;

namespace Grafos.Repository.Interfaces
{
    public interface IGraphRepository
    {
        Task<int> SaveGraph (Graph graph);

        Task<Graph> LoadGraph (int ID);

    }
}