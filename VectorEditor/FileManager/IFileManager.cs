using SDK;
using System.Collections.Generic;
using VectorEditor.UndoRedo;

namespace VectorEditor.FileManager
{
    /// <summary>
    /// Интерфейс для сохранения 
    /// и открытия проектов. 
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Сохранение фигур и истории
        /// команд в файл.
        /// </summary>
        /// <param name="figures">Фигуры</param>
        /// <param name="commands">Команды</param>
        /// <param name="redoCount">Количество Redo команд</param>
        void SaveToFile(string filename,
                        Dictionary<int, BaseFigure> figures, 
                        List<ICommand> commands,
                        int redoCount);

        /// <summary>
        /// Открытие проекта из файла
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <param name="commands">Команды</param>
        /// <param name="redoCount">Количество Redo команд</param>
        /// <returns>Список прочитанных фигур</returns>
        Dictionary<int, BaseFigure> OpenFromFile(string filename,
                                      out List<ICommand> commands,
                                      out int redoCount);
    }
}
