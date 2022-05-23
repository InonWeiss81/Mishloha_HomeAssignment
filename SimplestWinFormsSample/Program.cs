using DAL;
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
        

        //create the graph content 
        using (var a = new AdventureWorksEntities())
        {
            int i = 0;
            foreach (System.Reflection.TypeInfo ti in a.GetType().Assembly.GetTypes())
            {
                Node tempNode = new Node(i.ToString());
                tempNode.Attr.Shape = Shape.Box;
                tempNode.Label.Text = ti.Name;
                //foreach (System.Reflection.PropertyInfo pi in ti.DeclaredProperties)
                //{

                //}
                graph.AddNode(tempNode);

                i++;
            }
        }
        
        return graph;
    }
}