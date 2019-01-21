using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.UndoRedo
{
    /// <summary>
    /// Класс для стека команд
    /// </summary>
    public class UndoRedoStack
    {
        private Stack<ICommand> _undo;
        private Stack<ICommand> _redo;

        /// <summary>
        /// Конструктор класса стека команд
        /// </summary>
        public UndoRedoStack()
        {
            Reset();
        }

        /// <summary>
        /// Свойство: Количество команд в Undo
        /// </summary>
        public int UndoCount
        {
            get
            {
                return _undo.Count;
            }
        }

        /// <summary>
        /// Свойство: Количество команд в Redo
        /// </summary>
        public int RedoCount
        {
            get
            {
                return _redo.Count;
            }
        }

        /// <summary>
        /// Свойство: Стек команд Undo
        /// </summary>
        public Stack<ICommand> UndoStack
        {
            get
            {
                return _undo;
            }

            set
            {
                _undo = value;
            }
        }

        /// <summary>
        /// Свойство: Стек команд Redo
        /// </summary>
        public Stack<ICommand> RedoStack
        {
            get
            {
                return _redo;
            }

            set
            {
                _redo = value;
            }
        }

        /// <summary>
        /// Очистить стеки и создать заново
        /// </summary>
        public void Reset()
        {
            _undo = new Stack<ICommand>();
            _redo = new Stack<ICommand>();
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="cmd">Команда на выполнение</param>
        public void Do(ICommand cmd)
        {
            cmd.Do();

            _undo.Push(cmd);

            _redo.Clear();
        }

        /// <summary>
        /// Отменить
        /// </summary>
        public void Undo()
        {
            if (_undo.Count > 0)
            {
                ICommand cmd = _undo.Pop();

                cmd.Undo();

                _redo.Push(cmd);
            }
        }

        /// <summary>
        /// Выполнить заново
        /// </summary>
        public void Redo()
        {
            if (_redo.Count > 0)
            {
                ICommand cmd = _redo.Pop();

                cmd.Do();

                _undo.Push(cmd);
            }
        }
    }
}
