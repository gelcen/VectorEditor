using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.Figures;

namespace VectorEditor
{
    public partial class MainForm : Form
    {
        Color currentLineColor;
        Color currentFillColor;
        LineType currentLineType;
        bool drawing;
        int x, y, lx, ly = 0;
        Item currentItem;

        PolyLine polyLine;

        public MainForm()
        {
            InitializeComponent();
            currentLineColor = Color.Black;
            currentFillColor = Color.White;
            currentLineType = LineType.Solid;
            currentItem = Item.Cursor;
            polyLine = new PolyLine();
            drawing = false;
        }

        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            drawing = true;
            x = e.X;
            y = e.Y;            
            pbCanvas.Cursor = Cursors.Cross;            
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
            lx = e.X;
            ly = e.Y;
            switch (currentItem)
            {
                case Item.Line:
                    Line line = new Line(x, y, lx, ly, 
                                        Convert.ToInt32(nudLineThickness.Value), 
                                        currentLineColor, currentLineType);
                    LineDrawer lineDrawer = new LineDrawer(line, pbCanvas);
                    lineDrawer.Draw();
                    break;
                case Item.Polyline:
                    break;
                case Item.Polygon:
                    break;
                case Item.Circle:
                    float rad = (float)Math.Sqrt(Math.Pow((lx - x), 2) + Math.Pow((ly - y), 2));
                    Circle circle = new Circle(x, y, rad, currentLineColor,
                                              Convert.ToInt32(nudLineThickness.Value),
                                              currentFillColor, currentLineType);
                    CircleDrawer circleDrawer = new CircleDrawer(circle, pbCanvas);
                    circleDrawer.Draw();
                    break;
                case Item.Ellipse:
                    break;
                default:
                    break;
            }
            pbCanvas.Cursor = Cursors.Default;
        }

        private void pbCanvas_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonLine_Click(object sender, EventArgs e)
        {
            currentItem = Item.Line;
        }

        private void pbCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentItem == Item.Polyline)
            {
                x = e.X;
                y = e.Y;
                polyLine.SetProperties(Convert.ToInt32(nudLineThickness.Value),
                                        currentLineColor, currentLineType);
                polyLine.Add(x, y);
                PolyLineDrawer drawer = new PolyLineDrawer(polyLine, pbCanvas);
                drawer.Draw();
            }
            else
            {
                polyLine = new PolyLine();
            }
        }

        private void buttonPolyLine_Click(object sender, EventArgs e)
        {
            currentItem = Item.Polyline;
        }

        private void cbLineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbLineType.Text)
            {
                case "Сплошная":
                    currentLineType = LineType.Solid;
                    break;
                case "Пунктир":
                    currentLineType = LineType.Dashed;
                    break;
                default:
                    throw new Exception("Неверное значение типа линии.");
            }
        }

        private void buttonCursor_Click(object sender, EventArgs e)
        {
            currentItem = Item.Cursor;
        }

        private void buttonCircle_Click(object sender, EventArgs e)
        {
            currentItem = Item.Circle;
        }

        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void buttonLineColor_Click(object sender, EventArgs e)
        {
            if (colorDialogLineColor.ShowDialog()==DialogResult.OK)
            {
                buttonLineColor.BackColor = colorDialogLineColor.Color;
                currentLineColor = colorDialogLineColor.Color;
            }
        }

        private void buttonFillColor_Click(object sender, EventArgs e)
        {
            if(colorDialogLineColor.ShowDialog()==DialogResult.OK)
            {
                buttonFillColor.BackColor = colorDialogLineColor.Color;
                currentFillColor = colorDialogLineColor.Color;
            }
        }
    }
}
