using Newtonsoft.Json;
using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    /// <summary>
    /// Класс для команды движения фигур
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class MoveFigureCommand : ICommand
    {
        IModel _model;
        Dictionary<int, BaseFigure> _beforeState;
        Dictionary<int, BaseFigure> _newState;

        public IModel Model
        {
            get
            {
                return _model;
            }

            set
            {
                _model = value;
            }
        }

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
            _model = model;
            _beforeState = new Dictionary<int, BaseFigure>();
            _newState = new Dictionary<int, BaseFigure>();

            foreach (KeyValuePair<int, BaseFigure> entry in beforeState)
            {
                int index = entry.Key;
                _beforeState.Add(index, FigureFactory.CreateCopy(entry.Value));
            }
            foreach(KeyValuePair < int, BaseFigure > entry in newState)
            {
                int index = entry.Key;
                _newState.Add(index, FigureFactory.CreateCopy(entry.Value));
            }
        }

        /// <summary>
        /// Выполнить
        /// </summary>
        public void Do()
        {
            foreach (var figure in _newState)
            {
                _model.MoveFigure(figure.Key, figure.Value);
            }
        }

        /// <summary>
        /// Отменить
        /// </summary>
        public void Undo()
        {
            foreach (var figure in _beforeState)
            {
                _model.MoveFigure(figure.Key, figure.Value);
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
