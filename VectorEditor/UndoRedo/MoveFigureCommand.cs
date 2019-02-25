using Newtonsoft.Json;
using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для команды движения фигур
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class MoveFigureCommand : ICommand
    {
        /// <summary>
        /// Состояние до изменения
        /// </summary>
        private readonly Dictionary<int, BaseFigure> _beforeState;

        /// <summary>
        /// Состояние после изменения
        /// </summary>
        private readonly Dictionary<int, BaseFigure> _newState;

        public IModel Model { get; set; }

        /// <summary>
        /// Конструктор класса команды движения фигур
        /// </summary>
        /// <param name="model"></param>
        /// <param name="beforeState"></param>
        /// <param name="newState"></param>
        public MoveFigureCommand(IModel model, 
                                 Dictionary<int, BaseFigure> beforeState,
                                 Dictionary<int, BaseFigure> newState)
        {
            Model = model;
            _beforeState = new Dictionary<int, BaseFigure>();
            _newState = new Dictionary<int, BaseFigure>();

            foreach (var entry in beforeState)
            {
                var index = entry.Key;
                _beforeState.Add(index, FigureFactory.CreateCopy(entry.Value));
            }
            foreach(var entry in newState)
            {
                var index = entry.Key;
                _newState.Add(index, FigureFactory.CreateCopy(entry.Value));
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Выполнить
        /// </summary>
        public void Do()
        {
            foreach (var figure in _newState)
            {
                Model.MoveFigure(figure.Key, figure.Value);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Отменить
        /// </summary>
        public void Undo()
        {
            foreach (var figure in _beforeState)
            {
                Model.MoveFigure(figure.Key, figure.Value);
            }
        }

        /// <summary>
        /// Название команды
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Figure or figures are moved ";
        }
    }
}
