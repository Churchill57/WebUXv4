using StructureMap;
using StructureMap.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOLD.AppExecution.WebApi.Infrastructure
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();

                // Needed to resolve types in referenced assemblies (dlls).
                // Predicate here on specific assembliy names so as to exlude system and other dlls.
                scan.AssembliesFromApplicationBaseDirectory(assy => assy.FullName.StartsWith("GOLD"));
                //scan.AssembliesFromApplicationBaseDirectory(assy => assy.FullName.StartsWith("Customer"));

                // Resolve ISomeType --> SomeType
                scan.WithDefaultConventions();

                // WARNING
                // MVC doesn't allow a single instance of a controller to be reused (even for child requests) in a single http request,
                // so controllers should not be part of a container per request pattern. See StructureMapControllerConvention class.
                scan.With(new StructureMapControllerConvention());
            });
        }
    }
}