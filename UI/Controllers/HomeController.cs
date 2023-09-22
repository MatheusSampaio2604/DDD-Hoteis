using Application.Interfaces;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAcomodacaoApp _IAcomodacaoApp;

        public HomeController(ILogger<HomeController> logger, IAcomodacaoApp iAcomodacaoApp)
        {
            _logger = logger;
            _IAcomodacaoApp = iAcomodacaoApp;
        }

        public async Task<IActionResult> Index()
        {
            var i = await _IAcomodacaoApp.FindAllAsync();
            var iActive = i.Where(a => a.Ativo == true).ToList() ?? null;


            ViewBag.Acomodacoes = iActive ?? null;

            return View(iActive);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
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
