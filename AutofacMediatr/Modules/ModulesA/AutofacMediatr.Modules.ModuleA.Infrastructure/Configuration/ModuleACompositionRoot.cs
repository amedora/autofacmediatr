using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacMediatr.Modules.ModuleA.Infrastructure.Configuration
{
    public static class ModuleACompositionRoot
    {
        private static IContainer _container;

        public static void SetContainer(IContainer container) => _container = container;

        public static ILifetimeScope BeginLifetimeScope() => _container.BeginLifetimeScope();
    }
}
