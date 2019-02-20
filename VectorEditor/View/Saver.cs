using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VectorEditor.UndoRedo;

namespace VectorEditor.View
{
    /// <summary>
    /// Класс для сохранения и открытия данных
    /// </summary>
    public class Saver
    {
        /// <summary>
        /// Сохранения в файл
        /// </summary>
        /// <param name="undoRedoStack">Стек команд</param>
        /// <param name="filename">Путь к файлу</param>
        public void SaveToFile(UndoRedoStack undoRedoStack,                   
                               string filename)
        {            
            var file = new StreamWriter(filename);

            var redo = undoRedoStack.RedoStack;
            var undoCount = undoRedoStack.UndoCount;
            var redoCount = undoRedoStack.RedoCount;

            file.WriteLine(redoCount);

            for (int i = 0; i < undoCount; i++)
            {
                undoRedoStack.Undo();
            }

            var redoList = redo.ToList().AsReadOnly();

            foreach (var command in redoList)
            {
                var jsonString = JsonConvert.SerializeObject(command,
                                new JsonSerializerSettings
                                { TypeNameHandling = TypeNameHandling.All });
                file.WriteLine(jsonString);
            }

            for (int i = 0; i < undoCount; i++)
            {
                undoRedoStack.Redo();
            }

            file.Close();
        }

        /// <summary>
        /// Чтение из файла
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public FileLoadedEventArgs OpenFromFile(string filename)
        {
            var fileLoadedEventArgs = new FileLoadedEventArgs();
            var file = new StreamReader(filename);
            var undoCount = Convert.ToInt32(file.ReadLine());
            fileLoadedEventArgs.UndoCount = undoCount;

            var tempList = new List<ICommand>();

            while (!file.EndOfStream)
            {
                var line = file.ReadLine();
                var command = (ICommand)JsonConvert.DeserializeObject(line, 
                    new JsonSerializerSettings
                        { TypeNameHandling = TypeNameHandling.All });
                tempList.Add(command);
            }
            file.Close();
            fileLoadedEventArgs.RedoList = tempList;
            return fileLoadedEventArgs;
        }
    }
}
