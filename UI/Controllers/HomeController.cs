using Application.Interfaces;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
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
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AcomodacaoViewModel> i = await _IAcomodacaoApp.FindAllAsync();
            IEnumerable<AcomodacaoViewModel> iActive = i.Where(a => a.Ativo == true).ToList() ?? null;


            ViewBag.Acomodacoes = iActive ?? null;

            return View(iActive);
        }
        [HttpGet("Privacidade")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("Contato")]
        public IActionResult Contact()
        {
            return View();
        }

        // [HttpPost("Contato")]
        // public async Task<IActionResult> Contact(HomeViewModel model)
        // {
        //     // Valide o modelo, se necessário
        //     if (!ModelState.IsValid)
        //     {
        //         return View(model);
        //     }

        //     // Crie um objeto HttpClient para fazer a solicitação POST para o script PHP
        //     using (HttpClient client = new HttpClient())
        //     {
        //         string phpScriptUrl = "https://localhost:5001/Home/contact_process.php";

        //         // Crie os dados do formulário
        //     //     var formData = new List<KeyValuePair<string, string>>
        //     // {
        //     //     new KeyValuePair<string, string>("name", model.Name),
        //     //     new KeyValuePair<string, string>("email", model.Email),
        //     //     new KeyValuePair<string, string>("subject", model.Subject),
        //     //     new KeyValuePair<string, string>("message", model.Message)
        //     // };

        //         // Envie a solicitação POST
        //         // var response = await client.PostAsync(phpScriptUrl, new FormUrlEncodedContent(formData));

        //         // if (response.IsSuccessStatusCode)
        //         // {
        //         //     // A solicitação foi bem-sucedida
        //         //     // Você pode redirecionar para uma página de sucesso, exibir uma mensagem ou fazer o que for apropriado
        //         //     return View("Success");
        //         // }
        //         // else
        //         // {
        //         //     // A solicitação falhou, você pode redirecionar para uma página de erro ou exibir uma mensagem de erro
        //         //     return View("Error");
        //         // }
        //     }
        // }

        [HttpGet("PetFriendly")]
        public IActionResult Pet()
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
