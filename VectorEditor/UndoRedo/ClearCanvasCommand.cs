using Newtonsoft.Json;
using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для команды очистки канвы
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class ClearCanvasCommand : ICommand
    {
        /// <summary>
        /// Хранение удаленных фигур
        /// </summary>
        private readonly List<BaseFigure> _figures;

        /// <inheritdoc />
        /// <summary>
        /// Свойство модели
        /// </summary>
        public IModel Model { get; set; }

        /// <summary>
        /// Конструктор класса команды очистки канвы
        /// </summary>
        /// <param name="model">Модель</param>
        public ClearCanvasCommand(IModel model)
        {
            Model = model;
            _figures = new List<BaseFigure>();
            foreach (var figure in model.GetFigureList())
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

        /// <inheritdoc />
        /// <summary>
        /// Выполнить
        /// </summary>
        public void Do()
        {
            Model.ClearCanvas();
        }

        /// <inheritdoc />
        /// <summary>
        /// Отменить
        /// </summary>
        public void Undo()
        {
            foreach (var figure in _figures)
            {
                Model.AddFigure(figure);
            }
        }
    }
}
