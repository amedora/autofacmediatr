using Autofac;
using AutofacMediatr.BuildingBlocks.Application.DataAccess;
using AutofacMediatr.BuildingBlocks.Infrastructure.DataAccess;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacMediatr.Modules.ModuleA.Infrastructure.Configuration.DataAccess
{
    internal class DataAccessModule : Module
    {
        private readonly string _connectionString;

        internal DataAccessModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OracleConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _connectionString)
                .InstancePerLifetimeScope();

            builder
                .Register(c =>
                {
                    return new ModuleAUnitOfWork(_connectionString, c.Resolve<IMediator>());
                })
                .AsSelf()
                .InstancePerLifetimeScope();

            // このInfrastructureアセンブリで定義されている*Repositoryなクラスを、
            // それが実装しているインタフェースで依存性を解決できるようにする。
            var infrastructureAssembly = typeof(ModuleAUnitOfWork).Assembly;
            builder.RegisterAssemblyTypes(infrastructureAssembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());
        }
    }
}
