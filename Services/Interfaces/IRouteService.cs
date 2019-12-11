using System.Threading.Tasks;
using Grafos.Models;

namespace Grafos.Services.Interfaces
{
    public interface IRouteService
    {
        Task<RouteList> FindRoutesByGraphID (int graphID, string town1, string town2, int maxStops);

        Task<DistanceBetweenCities> FindMinimumDistanceByGraphID (int graphID, string town1, string town2);     
    }
}