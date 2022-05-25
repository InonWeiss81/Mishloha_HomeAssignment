using System.Collections.Generic;

namespace BL
{
    public class NodeData
    {
        public string Header { get; set; }

        public List<string> Items { get; set; }

        public NodeData()
        {
            Items = new List<string>();
        }
    }
}