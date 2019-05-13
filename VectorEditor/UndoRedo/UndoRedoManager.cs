using System.Collections.Generic;

namespace VectorEditor.UndoRedo
{
    /// <summary>
    /// Класс для стека команд
    /// </summary>
    public class UndoRedoManager
    {
        /// <summary>
        /// Конструктор класса стека команд
        /// </summary>
        public UndoRedoManager()
        {
            Reset();
        }

        /// <summary>
        /// Свойство: Количество команд в Undo
        /// </summary>
        public int UndoCount => UndoStack.Count;

        /// <summary>
        /// Свойство: Количество команд в Redo
        /// </summary>
        public int RedoCount => RedoStack.Count;

        /// <summary>
        /// Свойство: Стек команд Undo
        /// </summary>
        public Stack<ICommand> UndoStack { get; set; }

        /// <summary>
        /// Свойство: Стек команд Redo
        /// </summary>
        public Stack<ICommand> RedoStack { get; set; }

        /// <summary>
        /// Очистить стеки и создать заново
        /// </summary>
        public void Reset()
        {
            UndoStack = new Stack<ICommand>();
            RedoStack = new Stack<ICommand>();
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="cmd">Команда на выполнение</param>
        public void Do(ICommand cmd)
        {
            cmd.Do();

            UndoStack.Push(cmd);

            RedoStack.Clear();
        }

        /// <summary>
        /// Отменить
        /// </summary>
        public void Undo()
        {
            if (UndoStack.Count <= 0) return;
            var cmd = UndoStack.Pop();

            cmd.Undo();

            RedoStack.Push(cmd);
        }

        /// <summary>
        /// Выполнить заново
        /// </summary>
        public void Redo()
        {
            if (RedoStack.Count <= 0) return;
            var cmd = RedoStack.Pop();

            cmd.Do();

            UndoStack.Push(cmd);
        }
    }
}
