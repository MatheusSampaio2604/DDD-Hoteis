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
    public class HomeController : ControllerBase
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
            var i = await _IAcomodacaoApp.FindAllAsync();
            var iActive = i.Where(a => a.Ativo == true).ToList() ?? null;            

            return Ok(iActive);
        }
        [HttpGet("Privacidade")]
        public IActionResult Privacy()
        {
            return Ok();
        }

        [HttpGet("Contato")]
        public IActionResult Contact()
        {
            return Ok();
        }

        [HttpGet("PetFriendly")]
        public IActionResult Pet()
        {
            return Ok();
        }

    }
}
