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

            FileInfo[] drawerDlls = drawersFolder.GetFiles("*Drawer.dll");

            foreach (var drawerDll in drawerDlls)
            {
                var assembly = Assembly.LoadFrom(drawerDll.FullName);

                foreach (var type in assembly.DefinedTypes)
                {
                    if (type.Name.Contains("Drawer"))
                    {
                        int len = drawerDll.Name.IndexOf("Drawer.dll");
                        _drawerTypes.Add(
                            drawerDll.Name.Substring(0, len),
                            type.AsType());
                    }
                }
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
