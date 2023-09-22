using Application.Interfaces;
using Application.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class SuiteController : Controller
    {
        private readonly IAcomodacaoApp _IAcomodacaoApp;


        public SuiteController(IAcomodacaoApp iAcomodacaoApp)
        {
            _IAcomodacaoApp = iAcomodacaoApp;
        }

        public async Task<IActionResult> Index()
        {
            string frase = "Suíte";
            IEnumerable<AcomodacaoViewModel> obj = await _IAcomodacaoApp.FindAcomodacoesWithPhrase(frase);

            var i = await _IAcomodacaoApp.FindAllAsync();
            var iActive = i.Where(a => a.Ativo == true).ToList() ?? null;
            ViewBag.Acomodacoes = iActive ?? null;

            return View(obj);
        }

        // GET: ChaleController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var details = await _IAcomodacaoApp.FindOneAsync(id);
            return View(details);
        }


    }
}
