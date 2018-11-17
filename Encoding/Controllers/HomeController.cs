using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Encoding.Controllers
{
    public class HomeController : Controller
    {    
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]
        public ActionResult Index(string name, string email, string password)
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:62067/api/")
            };

            var res = client.PostAsync($"encode/signup?name={name}&email={email}&password={password}",null).Result.Content.ReadAsStringAsync().Result;

            return Redirect("Encrypt");
            
        }

        public ActionResult Encrypt()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Encrypt(string encodeText,string encode)
        {
            Uri baseAdress = new Uri("http://localhost:62067/api/encode/");

            var client = new HttpClient()
            {
                BaseAddress = baseAdress
            };
            var result = client.PostAsync($"{encode}?text={encodeText}", null).Result.Content.ReadAsStringAsync().Result;
            ViewBag.Result = result.Trim('\"');
            return View("Encrypt");

        }
    }
}
