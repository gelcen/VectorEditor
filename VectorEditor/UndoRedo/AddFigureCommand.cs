﻿using Newtonsoft.Json;
using VectorEditor.Figures;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для команды добавления фигуры
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class AddFigureCommand:ICommand
    {
        /// <summary>
        /// Для хранения добавленной фигуры
        /// </summary>
        private readonly BaseFigure _figure;

        /// <inheritdoc />
        /// <summary>
        /// Свойство для модели
        /// </summary>
        public IModel Model { get; set; }

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

        /// <inheritdoc />
        /// <summary>
        /// Выполнить
        /// </summary>
        public void Do()
        {
            Model.AddFigure(_figure);
        }

        /// <inheritdoc />
        /// <summary>
        /// Отменить
        /// </summary>
        public void Undo()
        {
            Model.DeleteFigure(_figure);
        }

    }

}
