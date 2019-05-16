using System;
using System.Windows.Forms;
using VectorEditor.Model;
using VectorEditor.FileManager;
using VectorEditor.Figures;
using VectorEditor.Drawers;
using Autofac;

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

            //var figureFactory = new GenericFigureFactory();
            //var drawerFactory = new DrawerFactory();
            //var drawerFacade = new FigureDrawerFacade(drawerFactory);
            //var mainForm = new MainForm(figureFactory,
            //                            drawerFacade);
            //var figureModel = new FigureModel();
            //var fileManager = new JsonFileManager();            

            //var presenter = new Presenter.Presenter(mainForm, 
            //                                        figureModel, 
            //                                        fileManager,
            //                                        figureFactory,
            //                                        drawerFacade);

            var container = ContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var presenter = scope.Resolve<Presenter.Presenter>();
                presenter.Run();
            }

            //Application.Run(mainForm);
        }
    }
}
