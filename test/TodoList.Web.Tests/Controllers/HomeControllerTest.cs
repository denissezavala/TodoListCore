using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using TodoList.Controllers;
using TodoList.Data;
using TodoList.Data.Models;
using Xunit;

namespace TodoList.Web.Tests.Controllers
{
    public class HomeControllerTest
    {
        private readonly HomeController _controller;
        private readonly ITodoRepository _todoRepositoryMock;
        
        public HomeControllerTest()
        {
            _todoRepositoryMock = Substitute.For<ITodoRepository>();
            _controller = new HomeController(_todoRepositoryMock);
        }

        // [Fact]
        // public async Task TestIndexRetrievesTodos()
        // {
        //     var expectedTodos = new List<Todo> {new Todo("Buy milk"), new Todo("Get stamps")};
        //     _todoRepositoryMock.GetAll().Returns(expectedTodos);

        //     var result = await _controller.Index() as ViewResult;

        //     var model = result.Model as IEnumerable<Todo>;
       
        //     Assert.NotNull(model);
        //     Assert.Equal(expectedTodos, model);
        // }

        [Fact]
        public void Test() {
            Console.WriteLine("Testing...");
            Assert.True(false);
        }
    }
}
