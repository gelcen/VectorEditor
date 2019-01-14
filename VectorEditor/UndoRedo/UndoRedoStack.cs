using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor.UndoRedo
{
    public class UndoRedoStack
    {
        private Stack<ICommand> _undo;
        private Stack<ICommand> _redo;

        public UndoRedoStack()
        {
            Reset();
        }

        public int UndoCount
        {
            get
            {
                return _undo.Count;
            }
        }

        public int RedoCount
        {
            get
            {
                return _redo.Count;
            }
        }

        public void Reset()
        {
            _undo = new Stack<ICommand>();
            _redo = new Stack<ICommand>();
        }

        public void Do(ICommand cmd)
        {
            cmd.Do();

            _undo.Push(cmd);

            _redo.Clear();
        }

        public void Undo()
        {
            if (_undo.Count > 0)
            {
                ICommand cmd = _undo.Pop();

                cmd.Undo();

                _redo.Push(cmd);
            }
        }

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
