using Autofac;
using SDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VectorEditor
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            DirectoryInfo figuresFolder =
                new DirectoryInfo(Directory.GetCurrentDirectory());

            FileInfo[] figuresDlls = figuresFolder.GetFiles("*Figure.dll");

            foreach (var figureDll in figuresDlls)
            {
            //    builder.RegisterAssemblyTypes(Assembly.Load(figureDll.FullName))
            //        .Where(t => t.Name.Contains("Figure")).As(BaseFigure);
            }

            

            return builder.Build();

        }

        
    }
}
