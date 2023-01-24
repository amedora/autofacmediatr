using Autofac;
using Autofac.Diagnostics;
using AutofacMediatr.Modules.ModuleA.Infrastructure.Configuration.DataAccess;
using AutofacMediatr.Modules.ModuleA.Infrastructure.Configuration.Logging;
using AutofacMediatr.Modules.ModuleA.Infrastructure.Configuration.Mediation;
using AutofacMediatr.Modules.ModuleA.Infrastructure.Configuration.Processing;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacMediatr.Modules.ModuleA.Infrastructure.Configuration
{
    public class ModuleAStartup
    {
        private static IContainer _container;

        public static void Initialize(string connectionString,
            ILogger logger)
        {
            var moduleLogger = logger.ForContext("Module", "ModuleA");
            ConfigureContainer(connectionString, moduleLogger);
        }

        private static void ConfigureContainer(string connectionString,
            ILogger logger)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new LoggingModule(logger));
            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new ProcessingModule());
            //containerBuilder.RegisterModule(new MappingModule());
            containerBuilder.RegisterModule(new MediatorModule());
            ///containerBuilder.RegisterInstance(executionContextAccessor);

            //containerBuilder.RegisterType<IdentificationTagService>();

            _container = containerBuilder.Build();

            ModuleACompositionRoot.SetContainer(_container);

            // Autofac Debug
            var tracer = new DefaultDiagnosticTracer();
            tracer.OperationCompleted += (sender, args) =>
            {
                Trace.WriteLine(args.TraceContent);
            };

            // Subscribe to the diagnostics with your tracer.
            _container.SubscribeToDiagnostics(tracer);
        }
    }
}
