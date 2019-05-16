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
    public class GenericFigureFactory : IFactory<BaseFigure>
    {
        private Dictionary<string, Type> _figureDictionary;

        public GenericFigureFactory()
        {
            _figureDictionary = new Dictionary<string, Type>();

            DirectoryInfo figuresFolder =
                new DirectoryInfo(Directory.GetCurrentDirectory());

            FileInfo[] figuresDlls = figuresFolder.GetFiles("*Figure.dll");

            foreach (var figureDll in figuresDlls)
            {
                var assembly = Assembly.LoadFrom(figureDll.FullName);

                foreach (var type in assembly.DefinedTypes)
                {
                    if (type.Name.Contains("Figure"))
                    {
                        int len = figureDll.Name.IndexOf("Figure.dll");
                        _figureDictionary.Add(
                            figureDll.Name.Substring(0, len),
                            type.AsType());
                    }
                }                
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
