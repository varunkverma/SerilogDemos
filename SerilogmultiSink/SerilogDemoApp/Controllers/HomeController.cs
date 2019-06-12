using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SerilogDemoApp.Models;
using Serilog.Extensions;
using Microsoft.Extensions.Logging; // needs to be added for ILogger
namespace SerilogDemoApp.Controllers
{
    public class HomeController : Controller
    {
        ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger){
            _logger=logger;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("Index was called-Info");
            _logger.LogError("Index was called-Error");
            _logger.LogWarning("Index was called-Debug");
            return View();
        }

        public IActionResult Privacy()
        {
            try{
                throw new Exception("error thrown");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Error in Privacy");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
