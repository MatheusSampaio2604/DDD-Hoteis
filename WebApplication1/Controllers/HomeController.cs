using Application.Interfaces;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            try
            {
                return Ok(await _IAcomodacaoApp.FindAllAsync());
            }
            catch (Exception)
            {
                return NotFound("Não foi encontrado Valores");
            }
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
