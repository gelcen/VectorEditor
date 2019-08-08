using VectorEditor.Models;

namespace VectorEditor.UndoRedo
{
    /// <summary>
    /// Фабрика для команд
    /// </summary>
    public static class CommandFactory
    {
        /// <summary>
        /// Восстановление ссылок на модель для команд
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="figureModel">Модель</param>
        public static void RestorePointersToModel(ICommand command, IModel figureModel)
        {                  
            if (command.GetType() == typeof(AddFigureCommand))
            {
                command.Model = figureModel;
            }
            else if (command.GetType() == typeof(ChangeParametersCommand))
            {
                command.Model = figureModel;
            }
            else if (command.GetType() == typeof(DeleteFigureCommand))
            {
                command.Model = figureModel;
            }
            else if (command.GetType() == typeof(MoveFigureCommand))
            {
                command.Model = figureModel;
            }
            else if (command.GetType() == typeof(MovePointCommand))
            {
                command.Model = figureModel;
            }
            
        }
    }
}
