using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.UndoRedo;

namespace VectorEditor.FileManager
{
    /// <summary>
    /// Интерфейс для сохранения 
    /// и открытия проектов. 
    /// </summary>
    interface IFileManager
    {
        /// <summary>
        /// Сохранение фигур и истории
        /// команд в файл.
        /// </summary>
        /// <param name="figures">Фигуры</param>
        /// <param name="commands">Команды</param>
        /// <param name="redoCount">Количество Redo команд</param>
        void SaveToFile(string filename,
                        List<BaseFigure> figures, 
                        List<ICommand> commands,
                        int redoCount);

        /// <summary>
        /// Открытие проекта из файла
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <param name="commands">Команды</param>
        /// <param name="redoCount">Количество Redo команд</param>
        /// <returns></returns>
        List<BaseFigure> OpenFromFile(string filename,
                                      out List<ICommand> commands,
                                      out int redoCount);
    }
}
