using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using TodoList.Data.Models;
using Xunit;

namespace TodoList.Web.IntegrationTests
{
    public class TodoApiTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public TodoApiTest()
        {
            var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;
            var contentRoot = GetProjectPath("TodoListCore.sln", startupAssembly);

            var builder = new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .ConfigureServices(InitializeServices)
                .UseEnvironment("Development")
                .UseStartup<Startup>();
            _server = new TestServer(builder);

            _client = _server.CreateClient();
            _client.BaseAddress = new Uri("http://localhost");
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }
        
        [Fact]
        public async Task AddAndRetrieveTodos()
        {
            // Arrange
            var buyApples = new Todo {Title = "Buy apples"};
            var content = new StringContent(JsonConvert.SerializeObject(buyApples), Encoding.UTF8, "application/json");

            // Act
            var postResponse = await _client.PostAsync("/api/todo", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
            
            // Act
            var getResponse = await _client.GetAsync("/api/todo");

            // Assert
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var jsonString = getResponse.Content.ReadAsStringAsync();
            jsonString.Wait();
            var todos = JsonConvert.DeserializeObject<List<Todo>>(jsonString.Result);

            Assert.Equal(3, todos.Count);
            Assert.Collection(todos, 
                t => Assert.Equal("Buy milk", t.Title),
                t => Assert.Equal("Get stamps", t.Title),
                t => Assert.Equal("Buy apples", t.Title)
            );

        }

        private void InitializeServices(IServiceCollection services)
        {
            services.AddDbContext<TodoListContext>(options => options.UseInMemoryDatabase());
        }

        private string GetProjectPath(string solutionName, Assembly startupAssembly)
        {
            // Get name of the target project which we want to test
            var projectName = startupAssembly.GetName().Name;

            // Get currently executing test project path
            var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;

            // Find the folder which contains the solution file. We then use this information to find the target
            // project which we want to test.
            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                var solutionFileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, solutionName));
                if (solutionFileInfo.Exists)
                {
                    return Path.GetFullPath(Path.Combine(directoryInfo.FullName, projectName));
                }

                directoryInfo = directoryInfo.Parent;
            } while (directoryInfo.Parent != null);

            throw new Exception($"Solution root could not be located using application root {applicationBasePath}.");
        }
    }
}