using Autofac;
using SDK;
using VectorEditor.Drawers;
using VectorEditor.Figures;
using VectorEditor.FileManager;
using VectorEditor.Models;

namespace VectorEditor
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<FigureFactory>().As<IFactory<BaseFigure>>();

            builder.RegisterType<DrawerFactory>().As<IFactory<BaseDrawer>>();

            builder.RegisterType<FigureDrawerFacade>().As<IDrawerFacade>();

            builder.RegisterType<MainForm>().As<IView>();

            builder.RegisterType<JsonFileManager>().As<IFileManager>();

            builder.RegisterType<FigureModel>().As<IModel>();

            builder.RegisterType<Presenter.Presenter>().AsSelf();

            return builder.Build();
        }        
    }
}
