namespace Grafos.Models
{
    public class RouteBetweenCities
    {
        public string Route { get; set; }

        public int Stops  { get; set; }

        public int Distance  { get; set; }

        public  RouteBetweenCities()
        {
        }
        public  RouteBetweenCities(string _route, int _distance, int _stops)
        {
            Route = _route;
            Distance = _distance;
            Stops = _stops;
        }
    }
}