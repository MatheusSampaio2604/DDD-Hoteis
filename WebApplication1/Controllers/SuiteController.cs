using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
            System.Collections.Generic.IEnumerable<Application.ViewModel.AcomodacaoViewModel> i = await _iAcomodacaoApp.FindAllAsync();

            JsonSerializerOptions jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                MaxDepth = 64
            };

            string json = JsonSerializer.Serialize(i, jsonOptions);

            return Ok(json);
        }

        [HttpGet("Detalhes")]
        public async Task<ActionResult> Details(int id)
        {
            Application.ViewModel.AcomodacaoViewModel result = await _iAcomodacaoApp.FindOneAsync(id);

            JsonSerializerOptions jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                MaxDepth = 64,
            };

            string json = JsonSerializer.Serialize(result, jsonOptions);
            return Ok(json);
        }
    }
}
