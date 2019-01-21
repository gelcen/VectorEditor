using System;
using Newtonsoft.Json;
using VectorEditor.Figures;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    /// <summary>
    /// Класс для команды добавления фигуры
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class AddFigureCommand:ICommand
    {
        private IModel _model;
        private BaseFigure _figure;

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
        /// Конструктор класса команды добавления фигуры
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="figure">Фигура для добавления</param>
        public AddFigureCommand(IModel model, BaseFigure figure)
        {
            Model = model;
            _figure = figure;
        }

        /// <summary>
        /// Название команды
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Added " + _figure.GetType();
        }

        /// <summary>
        /// Выполнить
        /// </summary>
        public void Do()
        {
            _model.AddFigure(_figure);
        }

        /// <summary>
        /// Отменить
        /// </summary>
        public void Undo()
        {
            _model.DeleteFigure(_figure);
        }

    }

}
