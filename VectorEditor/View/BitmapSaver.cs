using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using VectorEditor.Drawers;
using VectorEditor.Figures;

namespace VectorEditor.FileManager
{
    /// <summary>
    /// Класс для сохранения проекта в растровом формате
    /// </summary>
    public class BitmapSaver
    {
        public void SaveImage(Size size, Dictionary<int, BaseFigure> figures)
        {
            var pictureBox = new PictureBox { Size = size };
            var bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            var bitmapGraphics = Graphics.FromImage(bitmap);            

            foreach (var figure in figures)
            {
                FigureDrawer.DrawFigure(figure.Value, bitmapGraphics);
            }

            pictureBox.BackColor = Color.White;
            pictureBox.Image = bitmap;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Сохранить как...",
                OverwritePrompt = true,
                CheckPathExists = true,
                Filter = "PNG(*.png)|*.png|JPG (*.jpg,*.jpeg)|*.jpg;*.jpeg|BMP(*.bmp)|*.bmp|" +
                          "GIF(*.gif)|*.gif|TIFF (*.tif,*.tiff)|*.tif;*.tiff",
                ShowHelp = true                
            };            

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                pictureBox.Image.Save(saveFileDialog.FileName);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
