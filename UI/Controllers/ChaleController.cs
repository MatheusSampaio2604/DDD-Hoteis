using Application.Interfaces;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
            return View(await _IAcomodacaoApp.FindAcomodacoesWithPhrase("Chalé"));
        }


        [HttpGet("Detalhes")]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
            return View(await _IAcomodacaoApp.FindOneAsync(id));
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }



    }
}
