using Newtonsoft.Json;
using System.Collections.Generic;

namespace Grafos.Models
{
    public class DistanceBetweenCities
    {
         [JsonProperty(PropertyName = "distance")]
        public int Distance { get; set; }
        public List<string> Path { get; set; }
        
        public DistanceBetweenCities()
        {
            Path = new List<string>();
        }

        public DistanceBetweenCities(int _distance, List<string> _path)
        {
            Path = _path;
            Distance = _distance;
        }
    }
}