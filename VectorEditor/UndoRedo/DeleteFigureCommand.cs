using Newtonsoft.Json;
using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    /// <inheritdoc />
    /// <summary>
    /// Команда удаления фигуры
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class DeleteFigureCommand : ICommand
    {
        public IModel Model { get; set; }

        /// <summary>
        /// Словарь для хранения удалённых фигур
        /// </summary>
        private readonly Dictionary<int, BaseFigure> _deletedFigures;

        public DeleteFigureCommand(IModel model, Dictionary<int, BaseFigure> deletedFigures)
        {
            Model = model;
            _deletedFigures = deletedFigures;            
        }

        /// <summary>
        /// Названия команды
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Deleted figure or figures";
        }

        /// <inheritdoc />
        /// <summary>
        /// Выполнение команды
        /// </summary>
        public void Do()
        {
            foreach (var figure in _deletedFigures)
            {
                Model.DeleteFigure(figure.Value);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Отмена команды
        /// </summary>
        public void Undo()
        {
            foreach (var figure in _deletedFigures)
            {
                Model.AddFigure(figure.Value);
            }
        }
    }
}
