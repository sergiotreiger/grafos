using System.Threading.Tasks;
using Grafos.Models;
using Grafos.Services.Interfaces;
using Grafos.Repository.Interfaces;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Grafos.Services.Implementations
{
    public class RouteService : IRouteService
    {
        private readonly IGraphRepository _graphRepository;

        private List<GraphData> caminho = new List<GraphData>();

        public RouteService(IGraphRepository graphRepository)
        {
             _graphRepository = graphRepository;
        }
        
        public async Task<RouteList> FindRoutesByGraphID (int graphID, string town1, string town2, int maxStops)
        {
           RouteList routeCities = new RouteList();
           if (!town1.Equals(town2))
           {
                Graph graph = await _graphRepository.LoadGraph(graphID);
                if (graph.Data.Count > 0)
                {
                    Dictionary<string,List<GraphData>> neighborhoodDictionary = CreateNeighborhoodDictionary(graph);
                    Dictionary<string,bool> visitControlDictionary = CreateVisitControlDictionary(graph);
                    List<GraphData> temporaryPaths = CreateTemporaryPathsList (town1);
                    FindAllPaths(town1, town2, neighborhoodDictionary, temporaryPaths, maxStops, visitControlDictionary, routeCities);
                }
                else{
                     routeCities.Routes.Add (new RouteBetweenCities($"{town1}{town2}", 0, 0));
                }
            }
            else
            {
                routeCities.Routes.Add (new RouteBetweenCities(town1, 0, 0));
            }
           return routeCities;
        }

        public async Task<DistanceBetweenCities> FindMinimumDistanceByGraphID (int graphID, string town1, string town2)
        {
            RouteList routeCities = await FindRoutesByGraphID (graphID, town1,  town2, 0);
            DistanceBetweenCities distanceBetweenCities = new DistanceBetweenCities();

            if (routeCities.Routes.Count > 0)
            {
                distanceBetweenCities = FindMinimumDistance(routeCities);
            }
            return distanceBetweenCities;

        }

        private Dictionary<string,List<GraphData>>CreateNeighborhoodDictionary(Graph graph)
        {
            Dictionary<string,List<GraphData>> neighborhoodDictionary = new Dictionary<string, List<GraphData>>();
            foreach (GraphData graphData in graph.Data)
            {
                if (neighborhoodDictionary.ContainsKey(graphData.Source))
                {
                    neighborhoodDictionary[graphData.Source].Add(graphData);
                }
                else
                {
                    List<GraphData> lGraphItem = new List<GraphData>();
                    lGraphItem.Add(graphData);
                    neighborhoodDictionary.Add(graphData.Source, lGraphItem);
                }
            }
            return neighborhoodDictionary;
        }
                
      private Dictionary<string,bool>CreateVisitControlDictionary(Graph graph)
        {
            Dictionary<string,bool> visitControlDictionary = new Dictionary<string, bool>();
            foreach (GraphData graphData in graph.Data)
            {
                string key = $"{graphData.Source}{graphData.Target}";
                if (!visitControlDictionary.ContainsKey(graphData.Source))
                {
                    visitControlDictionary.Add(key, false);
                }
            }
            return visitControlDictionary;
        }                

        private List<GraphData> CreateTemporaryPathsList (string town1)
        {
            List<GraphData> tempPaths = new List<GraphData>();
            GraphData graphData = new GraphData();
            graphData.Source = town1;
            graphData.Target = town1;
            tempPaths.Add(graphData);
            return tempPaths;
        }
                
        private void FindAllPaths(string origem, 
                                  string destino,
                                  Dictionary<string,List<GraphData>> neighborhoodDictionary,
                                  List<GraphData> temporaryPaths,
                                  int maxStops,
                                  Dictionary<string,bool> visitControlDictionary,
                                  RouteList routeCities  )
        {
            List<GraphData> lista = neighborhoodDictionary[origem];
            foreach (GraphData graphItem in lista)
            {
                string keyPath = $"{graphItem.Source}{graphItem.Target}";

                if (!visitControlDictionary[keyPath])
                {
                    visitControlDictionary[keyPath] = true;
                    temporaryPaths.Add(graphItem);

                    if (graphItem.Target.Equals(destino))
                    {
                        if ((temporaryPaths.Count - 1 <= maxStops)|| maxStops==0)
                        {
                            AddRouteCities(temporaryPaths,routeCities);
                            temporaryPaths.RemoveAt(temporaryPaths.Count - 1);
                            visitControlDictionary[keyPath] = false;
                        }
                    }
                    else
                        {
                            FindAllPaths(graphItem.Target, destino, neighborhoodDictionary, temporaryPaths, maxStops, visitControlDictionary,routeCities);
                            visitControlDictionary[keyPath] = false;
                        }
                }
            }
            temporaryPaths.RemoveAt(temporaryPaths.Count - 1);
        }

        private void  AddRouteCities(List<GraphData> temporaryPaths, RouteList routeCities)
        {
            StringBuilder sRoute = new StringBuilder("");
            int distance = 0;
            int stops = 0;
            foreach (GraphData graphData in temporaryPaths)
            {
                sRoute.Append(graphData.Target);
                distance += graphData.Distance;
                stops++;
            }
            RouteBetweenCities routeBetweenCities = new RouteBetweenCities(sRoute.ToString(),distance,stops-1);
            routeCities.Routes.Add(routeBetweenCities);
        }

        private DistanceBetweenCities FindMinimumDistance( RouteList routeCities)
        {
           List<RouteBetweenCities> SortedList = routeCities.Routes.OrderBy(dvs=>dvs.Distance).ToList();
           DistanceBetweenCities distance = new DistanceBetweenCities();
            distance.Distance = SortedList[0].Distance;
            distance.Path.AddRange( SortedList[0].Route.Select(c => c.ToString() ));
            return distance;
        }
    }
}