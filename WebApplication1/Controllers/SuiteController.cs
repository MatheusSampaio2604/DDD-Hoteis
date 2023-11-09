using Application.Interfaces;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuiteController : ControllerBase
    {
        private readonly IAcomodacaoApp _IAcomodacaoApp;


        public SuiteController(IAcomodacaoApp iAcomodacaoApp)
        {
            _IAcomodacaoApp = iAcomodacaoApp;
        }
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            string frase = "Suíte";
            IEnumerable<AcomodacaoViewModel> obj = await _IAcomodacaoApp.FindAcomodacoesWithPhrase(frase);

            var i = await _IAcomodacaoApp.FindAllAsync();
            var iActive = i.Where(a => a.Ativo == true).ToList() ?? null;


            return Ok(obj);
        }

        [HttpGet("Detalhes")]
        public async Task<ActionResult> Details(int id)
        {
            var details = await _IAcomodacaoApp.FindOneAsync(id);
            return Ok(details);
        }

    }
}