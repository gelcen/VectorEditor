using Newtonsoft.Json;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    /// <summary>
    /// Класс для команды
    /// </summary>
    [JsonObject(MemberSerialization.Fields)]
    public interface ICommand
    {
        /// <summary>
        /// Модель 
        /// </summary>
        IModel Model
        {
            get;
            set;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        void Do();

        /// <summary>
        /// Отменить команду
        /// </summary>
        void Undo();
    }
}
