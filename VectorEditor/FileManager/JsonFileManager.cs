using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using VectorEditor.Figures;
using VectorEditor.UndoRedo;

namespace VectorEditor.FileManager
{
    /// <summary>
    /// Класс для сохранения и
    /// открытия проектов с 
    /// использованием JSON. 
    /// </summary>
    class JsonFileManager : IFileManager
    {
        /// <summary>
        /// Настройки сериализации
        /// </summary>
        private static readonly 
            JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        /// <summary>
        /// Сохранение
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="figures"></param>
        /// <param name="commands"></param>
        /// <param name="redoCount"></param>
        public void SaveToFile(string filename, List<BaseFigure> figures, List<ICommand> commands, int redoCount)
        {
            var file = new StreamWriter(filename);

            file.WriteLine(redoCount);

            foreach (var command in commands)
            {
                WriteItem(file, command);
            }

            foreach (var figure in figures)
            {
                WriteItem(file, figure);
            }

            file.Close();
        }

        /// <summary>
        /// Функция для записи
        /// объекта в файл
        /// </summary>
        /// <param name="file">Открытый файл для записи</param>
        /// <param name="item">Записываемый объект</param>
        private void WriteItem(StreamWriter file, object item)
        {
            var jsonString = JsonConvert.SerializeObject(
                                             item,
                                             _settings);
            file.WriteLine(jsonString);
        }

        /// <summary>
        /// Открытие
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="commands"></param>
        /// <param name="redoCount"></param>
        /// <returns></returns>
        public List<BaseFigure> OpenFromFile(string filename, out List<ICommand> commands, out int redoCount)
        {
            var figureList = new List<BaseFigure>();
            var commandsList = new List<ICommand>();

            var file = new StreamReader(filename);

            redoCount = Convert.ToInt32(file.ReadLine());

            while (!file.EndOfStream)
            {
                var line = file.ReadLine();

                object item = JsonConvert.DeserializeObject(
                                          line,
                                          _settings);

                switch (item)
                {
                    case ICommand command:
                        commandsList.Add(command);
                        break;
                    case BaseFigure figure:
                        figureList.Add(figure);
                        break;
                    default:
                        throw new ArgumentException(
                            message: "Wrong object",
                            paramName: nameof(item));
                }
            }

            commands = commandsList;
            return figureList;
        }

        
    }
}
