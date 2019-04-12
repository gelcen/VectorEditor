using System;
using System.Windows.Forms;
using VectorEditor.Model;
using VectorEditor.FileManager;

namespace VectorEditor
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new MainForm();
            var figureModel = new FigureModel();

            var presenter = new Presenter.Presenter(mainForm, figureModel);

            Application.Run(mainForm);
        }
    }
}
