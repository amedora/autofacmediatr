using AutofacMediatr.Modules.ModuleA.Application.TodoTasks.AddTodoTask;
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

        protected override void LoadTestData()
        {
            _fixture.LoadSeedData();
        }

        [Fact]
        public async Task TODOタスクを取得できる()
        {
            // Arrange
            var query = new GetTodoTasksQuery();

            // Act
            var result = await _moduleAModule.ExecuteQueryAsync(query);

            // Assert
            Assert.Equal("TODO 1", result.First().Todo);
        }

        [Fact]
        public async Task TODOタスクを作成できる()
        {
            // Arrange
            var command = new AddTodoCommand("new todo");

            // Act
            await _moduleAModule.ExecuteCommandAsync(command);

            // Assert
        }
    }
}
