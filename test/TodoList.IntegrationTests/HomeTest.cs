using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.TestHost;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Xunit;

namespace TodoList.IntegrationTests
{
    public class HomeTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public HomeTest()
        {
            var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;
            var contentRoot = GetProjectPath("TodoList.sln", "src", startupAssembly);

            var builder = new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .ConfigureServices(InitializeServices)
//                .UseEnvironment("Development")
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
        public async Task Home()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("hello", responseString);
        }

        private void InitializeServices(IServiceCollection services)
        {
            var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;

            // Inject a custom application part manager. Overrides AddMvcCore() because that uses TryAdd().
//            var manager = new ApplicationPartManager();
//            manager.ApplicationParts.Add(new AssemblyPart(startupAssembly));
//
//            manager.FeatureProviders.Add(new ControllerFeatureProvider());
//            manager.FeatureProviders.Add(new ViewComponentFeatureProvider());
//
//            services.AddSingleton(manager);

            services.AddMvc()
                .AddRazorOptions(options => {
                    var previous = options.CompilationCallback;
                    options.CompilationCallback = context => {
                        previous?.Invoke(context);
                        var references = MetadataReference.CreateFromFile(startupAssembly.Location);
                        context.Compilation = context.Compilation.AddReferences(references);
                    };
            });
        }

        private string GetProjectPath(string solutionName, string solutionRelativePath, Assembly startupAssembly)
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
                    return Path.GetFullPath(Path.Combine(directoryInfo.FullName, solutionRelativePath, projectName));
                }

                directoryInfo = directoryInfo.Parent;
            } while (directoryInfo.Parent != null);

            throw new Exception($"Solution root could not be located using application root {applicationBasePath}.");
        }
    }
}