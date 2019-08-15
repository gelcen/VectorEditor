using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using SDK;

namespace VectorEditorCore.Drawers
{
    /// <summary>
    /// Фабрика для создания рисовальщиков
    /// </summary>
    public class DrawerFactory : IFactory<BaseDrawer>
    {
        /// <summary>
        /// Словарь типов рисовальщиков
        /// </summary>
        private Dictionary<string, Type> _drawerTypes;

        /// <summary>
        /// Конструктор. 
        /// Загружает из папки запуска 
        /// exe-шника библиотеки фигур, 
        /// а из них типы рисовальщиков. 
        /// Использует name convention, т. е.
        /// Библиотеки фигур должны содержать 
        /// в конце слово Figure
        /// </summary>
        public DrawerFactory()
        {
            _drawerTypes = new Dictionary<string, Type>();

            DirectoryInfo drawersFolder =
                new DirectoryInfo(Directory.GetCurrentDirectory());

            // Name convention
            FileInfo[] drawerDlls = drawersFolder.GetFiles("*Figure.dll");

            foreach (var drawerDll in drawerDlls)
            {
                var assembly = Assembly.LoadFrom(drawerDll.FullName);

                IEnumerable<Type> types = GetLoadedTypes(assembly);

                foreach (var type in types)
                {
                    if (type.Name.Contains("Drawer"))
                    {
                        int length = drawerDll.Name.IndexOf("Figure.dll");
                        _drawerTypes.Add(
                            drawerDll.Name.Substring(0, length),
                            type);
                    }
                }
            }
        }

        /// <summary>
        /// Создание объекта рисовальщика
        /// </summary>
        /// <param name="name">Название рисовальщика</param>
        /// <returns></returns>
        public BaseDrawer CreateInstance(string name)
        {
            return (BaseDrawer)Activator.CreateInstance(
                _drawerTypes[name]);
        }

        /// <summary>
        /// Получить имена типов
        /// </summary>
        /// <returns>Список имен загруженных типов</returns>
        public List<string> GetNamesList()
        {
            return _drawerTypes.Keys.ToList();
        }

        /// <summary>
        /// Получить типы из сборки
        /// </summary>
        /// <param name="assembly">Сборка</param>
        /// <returns>Коллекция типов</returns>
        private static 
            IEnumerable<Type> GetLoadedTypes(Assembly assembly)
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
    }
}
