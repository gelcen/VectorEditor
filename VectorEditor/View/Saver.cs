using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using VectorEditor.Figures;
using VectorEditor.UndoRedo;

namespace VectorEditor.View
{
    /// <summary>
    /// Класс для сохранения и открытия данных
    /// </summary>
    public class Saver
    {

        public void SaveToFile(List<BaseFigure> figures,
                               Stack<ICommand> Undo,
                               Stack<ICommand> Redo,                    
                               string filename)
        {            
            var file = new StreamWriter(filename);
            file.WriteLine(figures.Count);
            file.WriteLine(Undo.Count);
            foreach (var figure in figures)
            {
                var jsonString = JsonConvert.SerializeObject(figure, 
                                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                file.WriteLine(jsonString);
            }
            foreach (var command in Undo)
            {
                var jsonString = JsonConvert.SerializeObject(command,
                                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                file.WriteLine(jsonString);
            }
            foreach (var command in Redo)
            {
                var jsonString = JsonConvert.SerializeObject(command,
                                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                file.WriteLine(jsonString);
            }
            file.Close();
        }

        public FileLoadedEventArgs OpenFromFile(string filename)
        {
            var fileLoadedEventArgs = new FileLoadedEventArgs();
            var file = new StreamReader(filename);
            int figuresCount = Convert.ToInt32(file.ReadLine());
            int undoCount = Convert.ToInt32(file.ReadLine());
            for (int i = 0; i < figuresCount; i++)
            {
                var line = file.ReadLine();
                var figure = (BaseFigure)JsonConvert.DeserializeObject(line,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                figure = FigureFactory.CreateCopy(figure);
                fileLoadedEventArgs.Figures.Add(figure);
            }
            for (int i = 0; i < undoCount; i++)
            {
                var line = file.ReadLine();
                var command = (ICommand)JsonConvert.DeserializeObject(line,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                fileLoadedEventArgs.UndoStack.Push(command);
            }
            while (!file.EndOfStream)
            {
                var line = file.ReadLine();
                var command = (ICommand)JsonConvert.DeserializeObject(line, 
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                fileLoadedEventArgs.RedoStack.Push(command);
            }
            file.Close();
            return fileLoadedEventArgs;
        }
    }
}
