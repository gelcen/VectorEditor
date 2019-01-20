using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditor.Figures;
using VectorEditor.Model;

namespace VectorEditor.UndoRedo
{
    /// <summary>
    /// Команда удаления фигуры
    /// </summary>
    public class DeleteFigureCommand : ICommand
    {
        /// <summary>
        /// Ссылка на модель
        /// </summary>
        private IModel _model;

        /// <summary>
        /// Словарь для хранения удалённых фигур
        /// </summary>
        private Dictionary<int, BaseFigure> _deletedFigures;

        public DeleteFigureCommand(IModel model, Dictionary<int, BaseFigure> deletedFigures)
        {
            _model = model;
            _deletedFigures = deletedFigures;            
        }

        /// <summary>
        /// Названия команды
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Deleted figure or figures";
        }

        /// <summary>
        /// Выполнение команды
        /// </summary>
        public void Do()
        {
            foreach (var figure in _deletedFigures)
            {
                _model.DeleteFigure(figure.Value);
            }
        }

        /// <summary>
        /// Undo команды
        /// </summary>
        public void Undo()
        {
            foreach (var figure in _deletedFigures)
            {
                _model.AddFigure(figure.Value);
            }
        }
    }
}
