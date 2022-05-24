using DAL;
using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Drawing;

class ViewerSample
{
    public static void Main()
    {
        //create a form 
        System.Windows.Forms.Form form = new System.Windows.Forms.Form();
        form.Width = 800;
        form.Height = 800;
        //create a viewer object 
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
        //create a graph object and
        //bind the graph to the viewer 
        viewer.Graph = BuildGraph();
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

        //Node tempNode1 = new Node("test1");
        //tempNode1.Attr.Shape = Shape.Box;
        //tempNode1.Label.Text = "TEST";
        //tempNode1.Attr.FillColor = Color.DeepSkyBlue;

        //graph.AddNode(tempNode1);

        //Node tempNode2 = new Node("test2");
        //tempNode2.Attr.Shape = Shape.Box;
        //tempNode2.Label.Text = "TEST";

        //graph.AddNode(tempNode2);

        //return graph;

        //create the graph content 
        using (var a = new AdventureWorksEntities())
        {
            foreach (System.Reflection.TypeInfo ti in a.GetType().Assembly.GetTypes())
            {
                Node tempNode = new Node(ti.Name);
                tempNode.Attr.Shape = Shape.Box;
                tempNode.Label.Text = ti.Name + "\n";
                foreach (System.Reflection.PropertyInfo pi in ti.DeclaredProperties)
                {
                    tempNode.Label.Text += "\n" + pi.Name;
                }
                graph.AddNode(tempNode);
            }
        }
       
        return graph;
    }
}