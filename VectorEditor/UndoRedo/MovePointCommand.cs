using Newtonsoft.Json;
using System.Collections.Generic;
using VectorEditor.Figures;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    /// <inheritdoc />
    /// <summary>
    /// Класс для команды движения маркера
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public class MovePointCommand:ICommand
    {
        /// <summary>
        /// Состояние до изменения
        /// </summary>
        private readonly Dictionary<int, BaseFigure> _beforePointState;

        /// <summary>
        /// Новое состояние
        /// </summary>
        private readonly Dictionary<int, BaseFigure> _newPointState;

        /// <inheritdoc />
        /// <summary>
        /// Свойство для модели
        /// </summary>
        public IModel Model { get; set; }

        /// <summary>
        /// Конструктор класса команды движения маркера
        /// </summary>
        /// <param name="model">Ссылка на модель</param>
        /// <param name="beforePointState">Состояние до изменения</param>
        /// <param name="newPointState">Состояние после изменения</param>
        public MovePointCommand(IModel model,
                                 Dictionary<int, BaseFigure> beforePointState,
                                 Dictionary<int, BaseFigure> newPointState)
        {
            Model = model;
            _beforePointState = new Dictionary<int, BaseFigure>();
            _newPointState = new Dictionary<int, BaseFigure>();

            CopyToDictionary(beforePointState, _beforePointState);
            CopyToDictionary(newPointState, _newPointState);
        }

        /// <summary>
        /// Копирование словарей
        /// </summary>
        /// <param name="fromDictionary">Из словаря</param>
        /// <param name="toDictionary">В словарь</param>
        private static void CopyToDictionary(Dictionary<int, BaseFigure> fromDictionary,
                                            Dictionary<int, BaseFigure> toDictionary)
        {
            foreach (var entry in fromDictionary)
            {
                var index = entry.Key;
                toDictionary.Add(index, FigureFactory.CreateCopy(entry.Value));
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Выполнить
        /// </summary>
        public void Do()
        {
            foreach (var figure in _newPointState)
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
            foreach (var figure in _beforePointState)
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
            return "Point is moved ";
        }
    }
}
