using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChaleController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IAcomodacaoApp _iAcomodacaoApp;

        public ChaleController(HttpClient httpClient, IAcomodacaoApp iAcomodacaoApp)
        {
            _httpClient = httpClient;
            _iAcomodacaoApp = iAcomodacaoApp;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                Utils utils = new();
                return Ok(await _iAcomodacaoApp.FindAcomodacoesWithPhrase("Chalé"));
                // var i = await _iAcomodacaoApp.FindAcomodacoesWithPhrase("Chalé");
                // var jsonOptions = new JsonSerializerOptions
                // {
                //     ReferenceHandler = ReferenceHandler.Preserve,
                // };

                // var jsonString = JsonSerializer.Serialize(i, jsonOptions);

                // return Ok(jsonString);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
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
