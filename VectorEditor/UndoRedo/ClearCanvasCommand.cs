using Newtonsoft.Json;
using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    /// <summary>
    /// Класс для команды очистки канвы
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class ClearCanvasCommand : ICommand
    {
        private IModel _model;
        private List<BaseFigure> _figures;

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
        /// Конструктор класса команды очистки канвы
        /// </summary>
        /// <param name="model">Модель</param>
        public ClearCanvasCommand(IModel model)
        {
            _model = model;
            _figures = new List<BaseFigure>();
            foreach (var figure in model.getFigureList())
            {
                _figures.Add(FigureFactory.CreateCopy(figure));
            }
        }

        /// <summary>
        /// Название команды
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Canvas has been cleared";
        }

        /// <summary>
        /// Выполнить
        /// </summary>
        public void Do()
        {
            _model.ClearCanvas();
        }

        /// <summary>
        /// Отменить
        /// </summary>
        public void Undo()
        {
            foreach (var figure in _figures)
            {
                _model.AddFigure(figure);
            }
        }
    }
}
