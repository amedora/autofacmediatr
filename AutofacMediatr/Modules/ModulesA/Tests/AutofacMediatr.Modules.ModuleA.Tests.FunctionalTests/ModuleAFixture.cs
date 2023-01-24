using AutofacMediatr.Libs.TestUtilities.Infrastructure;
using AutofacMediatr.Modules.ModuleA.Application.Contracts;
using AutofacMediatr.Modules.ModuleA.Infrastructure;
using AutofacMediatr.Modules.ModuleA.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AutofacMediatr.Modules.ModuleA.Tests.FunctionalTests
{
    public class ModuleAFixture : IAsyncLifetime, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly string _csvBaseDirectory;

        internal TestDatabase Database { get; private set; }

        public ModuleAFixture()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            _csvBaseDirectory = Path.Combine(
                Path.GetDirectoryName(Assembly.GetAssembly(typeof(ModuleAFixture)).Location),
                "SeedData", "Csv");

            Database = new TestDatabase(_configuration["ModuleAConnectionString"], "TODO");
        }

        public IModuleAModule BuildModule(ITestOutputHelper outputHelper)
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.TestOutput(outputHelper, Serilog.Events.LogEventLevel.Verbose)
                .CreateLogger()
                .ForContext<ModuleAFixture>();

            //var mockExecutionContextAccessor = new Mock<IExecutionContextAccessor>();
            //mockExecutionContextAccessor
            //    .SetupGet(x => x.UserName)
            //    .Returns("TS12345");

            ModuleAStartup.Initialize(_configuration["ModuleAConnectionString"],
                logger);

            return new ModuleAModule();
        }

        public async Task InitializeAsync()
        {
            await Database.ResetAsync();
            LoadSeedData();
        }

        public void LoadSeedData()
        {
            //Database.Load<AbilityCsv>(Path.Combine(_csvBaseDirectory, "foo.csv"));
        }

        public Task DisposeAsync()
        {
            // do nothing
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // do nothing
        }
    }
}
