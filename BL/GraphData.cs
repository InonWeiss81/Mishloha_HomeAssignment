using System.Collections.Generic;

namespace BL
{
    public class GraphData
    {
        public List<NodeData> Nodes { get; set; }

        public List<EdgeData> Edges { get; set; }

        public GraphData()
        {
            Nodes = new List<NodeData>();
            Edges = new List<EdgeData>();
        }
    }
}
