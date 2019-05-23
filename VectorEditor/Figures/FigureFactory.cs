using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SDK;

namespace VectorEditor.Figures
{
    /// <summary>
    /// Фабрика для фигур
    /// </summary>
    public class FigureFactory : IFactory<BaseFigure>
    {
        private Dictionary<string, Type> _figureDictionary;

        public FigureFactory()
        {
            _figureDictionary = new Dictionary<string, Type>();

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
                        int len = figureDll.Name.Length - 10;
                        _figureDictionary.Add(
                            figureDll.Name.Substring(0, len),
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

        /// <summary>
        /// Создать экземпляр фигуры
        /// </summary>
        /// <param name="name">Имя фигуры</param>
        /// <returns>Экземпляр фигуры</returns>
        public BaseFigure CreateInstance(string name)
        {
            return (BaseFigure)Activator.CreateInstance(
                _figureDictionary[name]);
        }

        /// <summary>
        /// Получить имена фигур
        /// </summary>
        /// <returns></returns>
        public List<string> GetNamesList()
        {
            return _figureDictionary.Keys.ToList();
        }
    }
}
