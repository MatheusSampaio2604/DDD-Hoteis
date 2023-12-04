using Application.Interfaces;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    [Route("[controller]")]
    public class SuiteController : Controller
    {
        private readonly IAcomodacaoApp _IAcomodacaoApp;


        public SuiteController(IAcomodacaoApp iAcomodacaoApp)
        {
            _IAcomodacaoApp = iAcomodacaoApp;
        }
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            string frase = "Suíte";
            IEnumerable<AcomodacaoViewModel> obj = await _IAcomodacaoApp.FindAcomodacoesWithPhrase(frase);

            IEnumerable<AcomodacaoViewModel> i = await _IAcomodacaoApp.FindAllAsync();
<<<<<<< HEAD
            List<AcomodacaoViewModel> iActive = i.Where(a => a.Ativo == true).ToList() ?? null;
=======
            IEnumerable<AcomodacaoViewModel> iActive = i.Where(a => a.Ativo == true).ToList() ?? null;
>>>>>>> fc19d229c883c2ee1f8833f566af6425c7684dd7
            ViewBag.Acomodacoes = iActive ?? null;

            return View(obj);
        }

        [HttpGet("Details")]
        public async Task<ActionResult> Details(int id)
        {
            AcomodacaoViewModel details = await _IAcomodacaoApp.FindOneAsync(id);
            return View(details);
        }


    }
}
