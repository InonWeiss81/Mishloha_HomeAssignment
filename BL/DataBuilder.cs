using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using DAL;

namespace BL
{
    public class DataBuilder
    {
        public GraphData GetData()
        {
            GraphData result = new GraphData();

            using (var db = new AdventureWorksEntities())
            {
                var objectContext = ((IObjectContextAdapter)db).ObjectContext;
                var container = objectContext.MetadataWorkspace.GetEntityContainer(objectContext.DefaultContainerName, DataSpace.CSpace);

                foreach (var set in container.BaseEntitySets.Where(bes => bes.ElementType is EntityType))
                {
                    var properties = ((EntityType)set.ElementType).Properties;
                    NodeData nodeData = new NodeData
                    {
                        Header = set.Name,
                        Items = properties.Select(p => p.Name).ToList()
                    };
                    result.Nodes.Add(nodeData);

                    var navigationProperties = ((EntityType)(set.ElementType)).NavigationProperties;
                    foreach (var nav in navigationProperties)
                    {
                        RelationType relationType = GetRelationType(nav.ToEndMember.RelationshipMultiplicity, nav.FromEndMember.RelationshipMultiplicity);
                        EdgeData edgeData = new EdgeData
                        {
                            FromNode = nav.FromEndMember.Name,
                            ToNode = nav.ToEndMember.Name,
                            RelationType = relationType
                        };
                        if (!result.Edges.Exists(e => e.FromNode == edgeData.FromNode && e.ToNode == edgeData.ToNode))
                        {
                            result.Edges.Add(edgeData);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// this method calculates the enum result in a mathematical way. it also could have done it in a simple logic way.
        /// but for some reason, I liked this way better.
        /// </summary>
        /// <param name="sourceRelationshipMultiplicity"></param>
        /// <param name="endRelationshipMultiplicity"></param>
        /// <returns></returns>
        private RelationType GetRelationType(RelationshipMultiplicity sourceRelationshipMultiplicity, RelationshipMultiplicity endRelationshipMultiplicity)
        {
            RelationType result = RelationType.None;
            int calcHelper = (int)RelationType.None;
            switch (sourceRelationshipMultiplicity)
            {
                case RelationshipMultiplicity.ZeroOrOne:
                case RelationshipMultiplicity.One:
                    calcHelper = 10;
                    break;
                case RelationshipMultiplicity.Many:
                    calcHelper = 20;
                    break;
                default:
                    break;
            }
            switch (endRelationshipMultiplicity)
            {
                case RelationshipMultiplicity.ZeroOrOne:
                case RelationshipMultiplicity.One:
                    calcHelper += 1;
                    break;
                case RelationshipMultiplicity.Many:
                    calcHelper += 2;
                    break;
                default:
                    break;
            }
            result = (RelationType)calcHelper;
            return result;
        }
    }
}
