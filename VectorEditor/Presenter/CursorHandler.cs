using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorEditor.Figures;
using VectorEditor.View;

namespace VectorEditor.Presenter
{
    public class CursorHandler : IBaseHandler
    {
        private FigureParameters _figureParameters;

        private PictureBox _canvas;

        private BaseFigure _selectedFigure;


        public FigureParameters FigureParameters
        {
            set
            {
                _figureParameters = value;
            }
        }

        public PictureBox Canvas
        {
            get
            {
                return _canvas;
            }
            set
            {
                _canvas = value;
            }
        }

        public CursorHandler(PictureBox canvas, FigureParameters figureParameters)
        {
            FigureParameters = figureParameters;
            Canvas = canvas;

            _selectedFigure = null;
        }

        public event EventHandler<BaseFigure> FigureCreated;

        public void Draw(Graphics g)
        {
            
        }

        public void MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        public void MouseUp(object sender, MouseEventArgs e)
        {
            
        }
    }
}
