using AutofacMediatr.Modules.ModuleA.Application.TodoTasks.GetTodoTasks;
using AutofacMediatr.Modules.ModuleA.Tests.FunctionalTests.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AutofacMediatr.Modules.ModuleA.Tests.FunctionalTests.Applications.TodoTasks
{
    public class TodoTaskTests : TestBase
    {
        public TodoTaskTests(ModuleAFixture fixture, ITestOutputHelper outputHelper) : base(fixture, outputHelper)
        {

        }

        [Fact]
        public async Task TODO�^�X�N���擾�ł���()
        {
            // Arrange
            var query = new GetTodoTasksQuery();

            // Act
            var result = await _moduleAModule.ExecuteQueryAsync(query);

            // Assert
            Assert.Equal("TODO 1", result.First().Todo);
        }

        protected override void LoadTestData()
        {
            _fixture.LoadSeedData();
        }
    }
}
