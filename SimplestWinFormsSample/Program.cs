using Microsoft.Msagl.Drawing;
using BL;
using System.Windows.Forms;
using System;
using Microsoft.Msagl.GraphViewerGdi;

class ViewerSample
{
    static object selectedObjectAttr;
    static object selectedObject;
    static readonly ToolTip toolTip1 = new ToolTip()
    {
        Active = true,
        AutoPopDelay = 5000,
        InitialDelay = 1000,
        ReshowDelay = 500
    };
    static readonly GViewer viewer = new GViewer();
    public static void Main()
    {
        //create a form 
        Form form = new Form();
        form.Width = 800;
        form.Height = 800;

        //viewer object 
        viewer.Graph = BuildGraph();
        viewer.CurrentLayoutMethod = LayoutMethod.IcrementalLayout;
        viewer.ObjectUnderMouseCursorChanged += new EventHandler<ObjectUnderMouseCursorChangedEventArgs>(Viewer_ObjectUnderMouseCursorChanged);

        //associate the viewer with the form 
        form.SuspendLayout();
        viewer.Dock = DockStyle.Fill;
        form.Controls.Add(viewer);
        form.ResumeLayout();

        //show the form 
        form.ShowDialog();
    }

    private static Graph BuildGraph()
    {
        var graphData = new DataBuilder().GetData();

        Graph graph = BuildGraphFromData(graphData); 

        return graph;
    }

    private static Graph BuildGraphFromData(GraphData graphData)
    {
        Graph graph = new Graph("graph");

        // build nodes
        foreach (NodeData nodeData in graphData.Nodes)
        {
            Node node = CreateNode(nodeData);
            graph.AddNode(node);
        }

        // build edges
        foreach (EdgeData edgeData in graphData.Edges)
        {
            //Edge edge = CreateEdge(edgeData);
            Edge edge = graph.AddEdge(edgeData.FromNode, edgeData.ToNode);
            edge.Attr.ArrowheadAtSource = (int)(edgeData.RelationType) / 10 == 1 ? ArrowStyle.None : ArrowStyle.Diamond;
            edge.Attr.ArrowheadAtTarget = (int)(edgeData.RelationType) % 10 == 1 ? ArrowStyle.None : ArrowStyle.Diamond;
            edge.UserData = edgeData.Description();
        }

        return graph;
    }

    private static Node CreateNode(NodeData nodeData)
    {
        Node result = new Node(nodeData.Header);
        result.Attr.Shape = Shape.Box;
        result.Label.Text = nodeData.Header + "\n";
        foreach (string item in nodeData.Items)
        {
            result.Label.Text += "\n" + item;
        }
        return result;
    }

    private static void Viewer_ObjectUnderMouseCursorChanged(object sender, ObjectUnderMouseCursorChangedEventArgs e)
    {
        selectedObject = e.OldObject?.DrawingObject;

        if (selectedObject != null)
        {
            if (selectedObject is Edge)
                (selectedObject as Edge).Attr = selectedObjectAttr as EdgeAttr;
            else if (selectedObject is Node)
                (selectedObject as Node).Attr = selectedObjectAttr as NodeAttr;

            selectedObject = null;
        }

        if (viewer.SelectedObject == null)
        {
            viewer.SetToolTip(toolTip1, "");
        }
        else
        {
            selectedObject = viewer.SelectedObject;
            if (selectedObject is Edge edge)
            {
                selectedObjectAttr = edge.Attr.Clone();
                edge.Attr.Color = Color.Magenta;

                viewer.SetToolTip(toolTip1, (string)edge.UserData);
            }
            else if (selectedObject is Node node)
            {

                selectedObjectAttr = (viewer.SelectedObject as Node).Attr.Clone();
                (selectedObject as Node).Attr.Color = Color.Magenta;
                viewer.SetToolTip(toolTip1, node.LabelText);
            }
        }

        viewer.Invalidate();
    }
}