using Newtonsoft.Json;
using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;
using VectorEditor.View;

namespace VectorEditor.UndoRedo
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для команды изменения параметров фигуры
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class ChangeParametersCommand : ICommand
    {
        /// <summary>
        /// Состояние до изменения
        /// </summary>
        private readonly Dictionary<int, BaseFigure> _beforeState;

        /// <summary>
        /// Состояние после изменения
        /// </summary>
        private readonly FigureParameters _newParameters;

        /// <inheritdoc />
        /// <summary>
        /// Свойство для модели
        /// </summary>
        public IModel Model { get; set; }

        /// <summary>
        /// Конструктор класса команды изменения параметров
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="beforeState">Состояние до изменения</param>
        /// <param name="newParameters">Новые параметры</param>
        public ChangeParametersCommand(IModel model, 
                                       Dictionary<int, BaseFigure> beforeState, 
                                       FigureParameters newParameters)
        {
            Model = model;
            _beforeState = beforeState;
            _newParameters = newParameters;
        }

        /// <inheritdoc />
        /// <summary>
        /// Выполнить
        /// </summary>
        public void Do()
        {
            foreach (var figure in _beforeState)
            {
                Model.ChangeFigureParameters(figure.Key, _newParameters);                
            }
        }

        /// <summary>
        /// Название команды
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Changed figure parameters";
        }

        /// <inheritdoc />
        /// <summary>
        /// Отменить
        /// </summary>
        public void Undo()
        {
            foreach (var figure in _beforeState)
            {
                var parameters = GetParameters(figure.Value);
                Model.ChangeFigureParameters(figure.Key, parameters);
            }
        }

        /// <summary>
        /// Получение параметров фигуры
        /// </summary>
        /// <param name="figure">Фигура, параметры которой нужно получить</param>
        /// <returns>Параметры фигуры из аргумента</returns>
        private static FigureParameters GetParameters(BaseFigure figure)
        {
            //REWORK
            var parameters = new FigureParameters
            {
                FillColor = System.Drawing.Color.AliceBlue,
                LineColor = System.Drawing.Color.AliceBlue,
                LineThickness = 1,
                LineStyle = 2
            };
            //***

            parameters.LineColor = figure.LineProperties.Color;
            parameters.LineStyle = (int)figure.LineProperties.Style;
            parameters.LineThickness = figure.LineProperties.Thickness;
            if (figure.GetType() == typeof(Circle) ||
                figure.GetType() == typeof(Ellipse) ||
                figure.GetType() == typeof(Polygon))
            {
                var tempFigure = figure as FillableFigure;
                if (tempFigure != null) parameters.FillColor = tempFigure.FillProperty.FillColor;
            }
            return parameters;
        }
    }
}
