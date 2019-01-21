﻿using Newtonsoft.Json;
using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;
using VectorEditor.View;

namespace VectorEditor.UndoRedo
{
    /// <summary>
    /// Класс для команды изменения параметров фигуры
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class ChangeParametersCommand : ICommand
    {
        private IModel _model;
        private Dictionary<int, BaseFigure> _beforeState;
        private FigureParameters _newParameters;

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

        /// <summary>
        /// Выполнить
        /// </summary>
        public void Do()
        {
            foreach (var figure in _beforeState)
            {
                _model.ChangeFigureParameters(figure.Key, _newParameters);                
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

        /// <summary>
        /// Отменить
        /// </summary>
        public void Undo()
        {
            foreach (var figure in _beforeState)
            {
                FigureParameters parameters = parameters = GetParameters(figure.Value);
                _model.ChangeFigureParameters(figure.Key, parameters);
            }
        }

        /// <summary>
        /// Получение параметров фигуры
        /// </summary>
        /// <param name="figure">Фигура, параметры которой нужно получить</param>
        /// <returns>Параметры фигуры из аргумента</returns>
        private FigureParameters GetParameters(BaseFigure figure)
        {
            //REWORK
            FigureParameters parameters;
            parameters.FillColor = System.Drawing.Color.AliceBlue;
            parameters.LineColor = System.Drawing.Color.AliceBlue;
            parameters.LineThickness = 1;
            parameters.LineType = 2;
            //***

            parameters.LineColor = figure.LineProperties.Color;
            parameters.LineType = (int)figure.LineProperties.Style;
            parameters.LineThickness = figure.LineProperties.Thickness;
            if (figure.GetType() == typeof(Circle) ||
                figure.GetType() == typeof(Ellipse) ||
                figure.GetType() == typeof(Polygon))
            {
                var tempFigure = figure as FillableFigure;
                parameters.FillColor = tempFigure.FillProperty.FillColor;
            }
            return parameters;
        }
    }
}
