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
    [Route("[controller]")]
    public class ChaleController : Controller
    {
        private readonly IAcomodacaoApp _IAcomodacaoApp;


        public ChaleController(IAcomodacaoApp iAcomodacaoApp)
        {
            _IAcomodacaoApp = iAcomodacaoApp;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            string frase = "Chalé";
            IEnumerable<AcomodacaoViewModel> obj = await _IAcomodacaoApp.FindAcomodacoesWithPhrase(frase);

            IEnumerable<AcomodacaoViewModel> i = await _IAcomodacaoApp.FindAllAsync();
            IEnumerable<AcomodacaoViewModel> iActive = i.Where(a => a.Ativo == true).ToList() ?? null;
            ViewBag.Acomodacoes = iActive ?? null;

            return View(obj);
        }


        [HttpGet("Detalhes")]
        public async Task<ActionResult> Details(int id)
        {
            AcomodacaoViewModel details = await _IAcomodacaoApp.FindOneAsync(id);
            return View(details);
        }



    }
}
