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
    public class ChaleController : Controller
    {
        private readonly IAcomodacaoApp _IAcomodacaoApp;
        

        public ChaleController(IAcomodacaoApp iAcomodacaoApp)
        {
            _IAcomodacaoApp = iAcomodacaoApp;
        }

        public async Task<IActionResult> Index()
        {
            string frase = "Chalé";
            IEnumerable<AcomodacaoViewModel> obj = await _IAcomodacaoApp.FindAcomodacoesWithPhrase(frase);

            ViewBag.Acomodacoes = (await _IAcomodacaoApp.FindAllAsync()) ?? null;

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
