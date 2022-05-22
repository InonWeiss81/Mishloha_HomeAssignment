using System.Collections.Generic;
using Microsoft.Msagl.Drawing;

class ViewerSample
{
    public static void Main()
    {
        //create a form 
        System.Windows.Forms.Form form = new System.Windows.Forms.Form();
        //create a viewer object 
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        //create a graph object 
        Graph graph = BuildGraph();

        //bind the graph to the viewer 
        viewer.Graph = graph;
        //associate the viewer with the form 
        form.SuspendLayout();
        viewer.Dock = System.Windows.Forms.DockStyle.Fill;
        form.Controls.Add(viewer);
        form.ResumeLayout();
        //show the form 
        form.ShowDialog();
    }

    private static Graph BuildGraph()
    {
        Graph graph = new Graph("graph");
        //create the graph content 
        var testNode1 = graph.AddNode("t1");
        testNode1.Attr.Shape = Shape.Box;
        testNode1.Attr.FillColor = Color.AliceBlue;
        testNode1.Label.Text = "קטן";

        var testNode2 = graph.AddNode("t2");
        testNode2.Attr.Shape = Shape.Box;
        testNode2.Attr.FillColor = Color.LightPink;
        testNode2.Label.Text = "קדן";

        var testNode3 = graph.AddNode("t3");
        testNode3.Attr.Shape = Shape.Box;
        testNode3.Attr.FillColor = Color.AntiqueWhite;
        testNode3.Attr.Shape = Shape.House;
        testNode3.Label.Text = "בית";

        graph.AddEdge("t1", "t3");
        graph.AddEdge("t2", "t3");
        return graph;
    }
}