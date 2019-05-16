using SDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.Figures
{
    public class NewFigureFactory
    {
        private Dictionary<string, Type> _figureTypes =
                new Dictionary<string, Type>();

        public NewFigureFactory()
        {
            DirectoryInfo figuresFolder =
                new DirectoryInfo(Directory.GetCurrentDirectory());

            FileInfo[] figuresDlls = figuresFolder.GetFiles("*Figure.dll");

            foreach (var figureDll in figuresDlls)
            {
                var assembly = Assembly.LoadFrom(figureDll.FullName);

                IEnumerable<Type> types = GetLoadedTypes(assembly);

                foreach (var type in types)
                {
                    if (type.Name.Contains("Figure"))
                    {
                        int len = figureDll.Name.Length - 4;
                        _figureTypes.Add(
                            figureDll.Name.Substring(0, len), type);
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

        public BaseFigure CreateFigure(string name)
        {            
            return (BaseFigure)Activator.CreateInstance(
                _figureTypes[name]);
        }

        public List<string> GetFigureNames()
        {
            return _figureTypes.Keys.ToList();
        }
    }
}
