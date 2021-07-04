using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Arbetsprov.Models;
using System.Net.Http;

namespace Arbetsprov.Controllers {
    public class HomeController : Controller {

        private static readonly HttpClient client = new HttpClient();
        public async Task<IActionResult> IndexAsync(string query = null)
        {

            if (query == null)
            {
                ViewData["Message"] = "Write your query below and press search.";
                return View();
            }
            if (query.Length < 38 || query.Substring(0, 38) != "http://api.libris.kb.se/xsearch?query=")
            {
                ViewData["Message"] = "Incorrect query: " + query + "\n.Query has to start with: http://api.libris.kb.se/xsearch?query=";
                return View();
            }

            var listOfSearchResults = await new ApiController().SearchAsync(query);
            return View(listOfSearchResults);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
