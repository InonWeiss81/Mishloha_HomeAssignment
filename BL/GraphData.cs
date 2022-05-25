using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
