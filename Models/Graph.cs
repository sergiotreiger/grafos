using System.Collections.Generic;

namespace Grafos.Models
{
    public class Graph
    {
        //TODO colocar os demais atributos
        public int ID { get; set; }

        public ICollection<GraphData> Data { get; set; }

        public Graph()
        {
            Data = new List<GraphData>();
        }
    }
}