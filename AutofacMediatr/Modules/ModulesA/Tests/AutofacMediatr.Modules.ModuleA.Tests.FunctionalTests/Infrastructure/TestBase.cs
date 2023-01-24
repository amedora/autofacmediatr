using AutofacMediatr.Modules.ModuleA.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AutofacMediatr.Modules.ModuleA.Tests.FunctionalTests.Infrastructure
{
    [Collection(nameof(ModuleACollection))]
    public abstract class TestBase : IAsyncLifetime
    {
        protected readonly ModuleAFixture _fixture;
        protected readonly IModuleAModule _moduleAModule;

        public TestBase(ModuleAFixture fixture, ITestOutputHelper outputHelper)
        {
            _fixture = fixture;
            _moduleAModule = _fixture.BuildModule(outputHelper);
        }

        public async Task InitializeAsync()
        {
            await _fixture.Database.ResetAsync();
            LoadTestData();
        }

        protected abstract void LoadTestData();

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
