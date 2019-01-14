namespace VectorEditor.UndoRedo
{
    public interface ICommand
    {
        void Do();

        void Undo();
    }
}
