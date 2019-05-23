using SDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.Drawers
{
    public class DrawerFactory : IFactory<BaseDrawer>
    {
        private Dictionary<string, Type> _drawerTypes;

        public DrawerFactory()
        {
            _drawerTypes = new Dictionary<string, Type>();

            DirectoryInfo drawersFolder =
                new DirectoryInfo(Directory.GetCurrentDirectory());

            FileInfo[] drawerDlls = drawersFolder.GetFiles("*Figure.dll");

            foreach (var drawerDll in drawerDlls)
            {
                var assembly = Assembly.LoadFrom(drawerDll.FullName);

                IEnumerable<Type> types = GetLoadedTypes(assembly);

                foreach (var type in types)
                {
                    if (type.Name.Contains("Drawer"))
                    {
                        int len = drawerDll.Name.IndexOf("Figure.dll");
                        _drawerTypes.Add(
                            drawerDll.Name.Substring(0, len),
                            type);
                    }
                }
            }
        }

        private static IEnumerable<Type> GetLoadedTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }

        public BaseDrawer CreateInstance(string name)
        {
            return (BaseDrawer)Activator.CreateInstance(
                _drawerTypes[name]);
        }

        public List<string> GetNamesList()
        {
            return _drawerTypes.Keys.ToList();
        }
    }
}
