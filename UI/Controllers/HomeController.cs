using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
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
            ViewBag.Acomodacoes = (await _IAcomodacaoApp.FindAllAsync()) ?? null;
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
