using System.Diagnostics;
using System.Text;
using Company.DEMO.PL.Models;
using Company.DEMO.PL.Models.SERVICES;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.DEMO.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService scopedService1;
        private readonly IScopedService scopedService2;
        private readonly ITranseintService transeintService1;
        private readonly ITranseintService transeintService2;
        private readonly ISingletonService singletonService1;
        private readonly ISingletonService singletonService2;

        public HomeController(ILogger<HomeController> logger,IScopedService scopedService1,IScopedService scopedService2,ITranseintService transeintService1,ITranseintService transeintService2,ISingletonService singletonService1,ISingletonService singletonService2)
        {
            _logger = logger;
            this.scopedService1 = scopedService1;
            this.scopedService2 = scopedService2;
            this.transeintService1 = transeintService1;
            this.transeintService2 = transeintService2;
            this.singletonService1 = singletonService1;
            this.singletonService2 = singletonService2;
        }

        public IActionResult Index()
        {
            return View();
        }
        public string TestlifeTime()
        {
           StringBuilder builder = new StringBuilder();
            builder.Append($"ScopedService1: {scopedService1.GetGuid()} \n");
            builder.Append($"ScopedService2: {scopedService2.GetGuid()}\n");
            builder.Append($"TranseintService1: {transeintService1.GetGuid()}\n");
            builder.Append($"TranseintService2: {transeintService2.GetGuid()}\n");
            builder.Append($"SingletonService1: {singletonService1.GetGuid()}\n");
            builder.Append($"SingletonService2: {singletonService2.GetGuid()}\n");
            return builder.ToString();
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
