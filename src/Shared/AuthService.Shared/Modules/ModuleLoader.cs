
using System.Reflection;

namespace AuthService.Shared.Modules
{
    public static class ModuleLoader
    {
        // private const string ModulePartBase = "AuthService.Modules.";
        public static IList<Assembly> LoadAssemblies()
        {
            //get all assamblies
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            //find non dynamis and return them location
            var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();
            //get all ddl files that are not already loaded into memory are included and checking if the location of each file is not contained in the list of assembly 
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll").Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
            .ToList();

            files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

            return assemblies;
        }

        //get scopeds of all modules
        public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
          => assemblies
              .SelectMany(x => x.GetTypes())
              .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
              .OrderBy(x => x.Name)
              .Select(Activator.CreateInstance)
              .Cast<IModule>()
              .ToList();
    }
}