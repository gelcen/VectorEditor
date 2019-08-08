using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorEditor.Model;

namespace VectorEditorUnitTests.Model
{
    [TestFixture]
    public class FigureModelTest
    {
        [Test]
        public void
            NewProject_InvokeNewProject_InitializesModelState()
        {
            IModel model = new FigureModel();

            model.NewProject();

            //List is initalized
            Assert.NotNull(model.GetFigureList());
            //Index is set to 0
            Assert.AreEqual(0, model.CurrentIndex);
            //IsChanged set to false
            Assert.AreEqual(false, model.IsChanged);
        }

        [Test]
        public void 
            AddFigure_FigureAdded_ReturnsListWithFigure()
        {
            IModel model = new FigureModel();
            model.NewProject();
            LineFigureMock figure = new LineFigureMock();

            model.AddFigure(0, figure);

            Assert.AreEqual(figure, model.GetFigureList()[0]);            
        }

        [Test]
        public void
            IsChanged_IsChangedAfterNewProject_ReturnsFalse()
        {
            IModel model = new FigureModel();

            model.NewProject();            

            Assert.AreEqual(false, model.IsChanged);
        }

        [Test]
        public void
            IsChanged_IsChangedAfterAddFigure_ReturnsTrue()
        {
            IModel model = new FigureModel();
            model.NewProject();
            LineFigureMock figure = new LineFigureMock();

            model.AddFigure(0, figure);

            Assert.AreEqual(true, model.IsChanged);
        }

        [Test]
        public void
            CopyFigure_CopyTheFigure_CopyWillBeAddedToList()
        {
            IModel model = new FigureModel();
            model.NewProject();
            LineFigureMock figure = new LineFigureMock();
            figure.LineProperties.Color = Color.AliceBlue;
            figure.LineProperties.Style = DashStyle.Solid;
            figure.LineProperties.Thickness = 1;
            figure.Points.AddPoint(new PointF(1, 2));
            figure.Points.AddPoint(new PointF(4, 5));

            model.CopyFigure(0, figure);

            //Assert.AreEqual(figure, model.GetFigureList()[1]);
            Assert.AreEqual(figure, model.GetFigureList()[1]);
        }


    }
}
