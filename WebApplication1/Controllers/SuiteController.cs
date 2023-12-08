using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuiteController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IAcomodacaoApp _iAcomodacaoApp;

        public SuiteController(HttpClient httpClient, IAcomodacaoApp iAcomodacaoApp)
        {
            _httpClient = httpClient;
            _iAcomodacaoApp = iAcomodacaoApp;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok(await _iAcomodacaoApp.FindAcomodacoesWithPhrase("Suíte"));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpGet("Detalhes")]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                return Ok(await _iAcomodacaoApp.FindOneAsync(id));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
    }
}
