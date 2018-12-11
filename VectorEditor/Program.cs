using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VectorEditor.Model;
using VectorEditor.Presenter;

namespace VectorEditor
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainForm mainForm = new MainForm();
            FigureModel figureModel = new FigureModel();

            Presenter.Presenter presenter = new Presenter.Presenter(mainForm, figureModel);

            Application.Run(mainForm);
        }
    }
}
