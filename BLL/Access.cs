using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using DAL;

namespace BL
{
    public class Access
    {
        public List<string> GetData()
        {
            List<string> result = new List<string>();

            using (var db = new AdventureWorksEntities())
            {
                var objectContext = ((IObjectContextAdapter)db).ObjectContext;
                var container = objectContext.MetadataWorkspace.GetEntityContainer(objectContext.DefaultContainerName, DataSpace.CSpace);

                foreach (var set in container.BaseEntitySets)
                {
                    var properties = ((EntityType)(set.ElementType)).Properties;
                    var navigationProperties = ((EntityType)(set.ElementType)).NavigationProperties;
                    foreach (var nav in navigationProperties)
                    {
                        
                    }
                }
            }
            
            return result;
        }
    }
}
