using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VectorEditor.Presenter
{
    public class EditingHandler
    {
        private Presenter _presenter;
        private IHandler _handler;
        private NewCursorHandler _cursorHandler;

        public EditingHandler(Presenter presenter,
                                IHandler handler,
                                NewCursorHandler cursorHandler)
        {
            _presenter = presenter;
            _handler = handler;
            _cursorHandler = cursorHandler;

            _handler.Draw += DrawHandler;
            _handler.MouseDown += MouseDownSelecting;
            _handler.MouseUp += MouseUpSelecting;
            _handler.MouseMove += MouseMoveSelecting;
        }

        private void MouseMoveSelecting(object arg1, MouseEventArgs arg2)
        {
            
        }

        private void MouseUpSelecting(object arg1, MouseEventArgs arg2)
        {
            
        }

        private void MouseDownSelecting(object arg1, MouseEventArgs arg2)
        {
            
        }

        private void DrawHandler(Graphics obj)
        {
            
        }
    }
}
