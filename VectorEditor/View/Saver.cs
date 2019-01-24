using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VectorEditor.Figures;
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
        /// <param name="figures"></param>
        /// <param name="undo"></param>
        /// <param name="redo"></param>
        /// <param name="filename"></param>
        public void SaveToFile(List<BaseFigure> figures,
                               Stack<ICommand> undo,
                               Stack<ICommand> redo,                    
                               string filename)
        {            
            var file = new StreamWriter(filename);
            file.WriteLine(figures.Count);
            file.WriteLine(undo.Count);
            var undoList = undo.ToList().AsReadOnly();
            var redoList = redo.ToList().AsReadOnly();
            foreach (var figure in figures)
            {
                var jsonString = JsonConvert.SerializeObject(figure, 
                                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                file.WriteLine(jsonString);
            }
            foreach (var command in undoList)
            {
                var jsonString = JsonConvert.SerializeObject(command,
                                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                file.WriteLine(jsonString);
            }
            foreach (var command in redoList)
            {
                var jsonString = JsonConvert.SerializeObject(command,
                                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                file.WriteLine(jsonString);
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
            var figuresCount = Convert.ToInt32(file.ReadLine());
            var undoCount = Convert.ToInt32(file.ReadLine());
            for (var i = 0; i < figuresCount; i++)
            {
                var line = file.ReadLine();
                var figure = (BaseFigure)JsonConvert.DeserializeObject(line,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                fileLoadedEventArgs.Figures.Add(figure);
            }
            List<ICommand> tempList = new List<ICommand>();
            for (var i = 0; i < undoCount; i++)
            {
                var line = file.ReadLine();
                var command = (ICommand)JsonConvert.DeserializeObject(line,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                tempList.Add(command);
            }
            for (var i = tempList.Count-1; i >= 0; i--)
            {
                fileLoadedEventArgs.UndoStack.Push(tempList[i]);
            }
            tempList.Clear();
            while (!file.EndOfStream)
            {
                var line = file.ReadLine();
                var command = (ICommand)JsonConvert.DeserializeObject(line, 
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                tempList.Add(command);
            }
            for (var i = tempList.Count - 1; i >= 0; i--)
            {
                fileLoadedEventArgs.RedoStack.Push(tempList[i]);
            }
            file.Close();
            return fileLoadedEventArgs;
        }
    }
}
