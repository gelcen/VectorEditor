namespace VectorEditor.Figures
{
    public class Line:BaseFigure
    {
        public Line()
        {
            _lineProperties = new LineProperties();
            _points = new Points(2);
        }
    }
}
