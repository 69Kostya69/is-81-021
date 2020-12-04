using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiTest.Models;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ApiTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult Index(GitModel model)
        {
            var client = new RestClient($"https://api.github.com/users/{model.Login}");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            if(response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<JToken>(response.Content);

                var followers = content["followers"].Value<int>();
                var following = content["following"].Value<int>();
                var id = content["id"].Value<int>();
                var dataCreate = content["created_at"].Value<string>();
                var repo = content["public_repos"].Value<int>();

                GitModel m = new GitModel 
                { 
                  Login = model.Login,

                  Id = id, 

                  Repo = repo,

                  Followers = followers,

                  Following = following, 

                  DataCreate = dataCreate
    
                };
                return View(m);
            }
            ViewBag.error = "No such user exists!";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
