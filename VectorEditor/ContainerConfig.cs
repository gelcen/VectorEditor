using Autofac;
using SDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VectorEditor.Drawers;
using VectorEditor.Figures;
using VectorEditor.FileManager;
using VectorEditor.Model;

namespace VectorEditor
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<GenericFigureFactory>().As<IFactory<BaseFigure>>();

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
