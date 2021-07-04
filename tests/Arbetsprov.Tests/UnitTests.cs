using System;
using Xunit;

using Arbetsprov;
using Arbetsprov.Models;
using Arbetsprov.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Arbetsprov.Tests {
    public class UnitTests {
        [Fact]
        public void Test1() {
            var query = "query";
            
            var controller = new ApiController();
            var actionResult = controller.SearchAsync(query) as ObjectResult;
            var result = actionResult.Value as SearchResult;

            Assert.Equal(result.Query, query);
        }
    }
}
