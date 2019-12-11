
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Grafos.Models
{
    public class RouteList
    {

        [JsonProperty(PropertyName = "routes")]
        public List<RouteBetweenCities> Routes { get; set; } 

        public RouteList()
        {
            Routes =  new List<RouteBetweenCities>();
        }
    }
}