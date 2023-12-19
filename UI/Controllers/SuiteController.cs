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
            return View(await _IAcomodacaoApp.FindAcomodacoesWithPhrase("Suíte"));
        }

        [HttpGet("Details")]
        public async Task<ActionResult> Details(int id)
        {
            return View(await _IAcomodacaoApp.FindOneAsync(id));
        }


    }
}
