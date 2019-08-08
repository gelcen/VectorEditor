using Newtonsoft.Json;
using SDK;
using VectorEditor.Models;

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
        /// Индекс добавленной фигуры
        /// </summary>
        private int _index;

        /// <summary>
        /// Для хранения добавленной фигуры
        /// </summary>
        public BaseFigure Figure => _figure;

        /// <summary>
        /// Поле для индекса
        /// </summary>
        public int Index
        {
            get
            {
                return _index;
            }

            set
            {
                _index = value;
            }
        }

        /// <summary>
        /// Конструктор класса команды добавления фигуры
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="figure">Фигура для добавления</param>
        /// <param name="index">Индекс</param>
        public AddFigureCommand(IModel model, BaseFigure figure, int index)
        {
            Model = model;
            _figure = figure;
            Index = index;
        }

        /// <summary>
        /// Название команды
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Added " + Figure.GetType();
        }

        /// <inheritdoc />
        /// <summary>
        /// Выполнить
        /// </summary>
        public void Do()
        {
            Model.AddFigure(Index, Figure);
        }

        /// <inheritdoc />
        /// <summary>
        /// Отменить
        /// </summary>
        public void Undo()
        {
            Model.DeleteFigureAt(Index, Figure);
        }

    }

}
