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
            Utils utils = new Utils();

            return Ok(utils.ListToJson(await _iAcomodacaoApp.FindAllAsync()));
        }



        [HttpGet("Detalhes")]
        public async Task<ActionResult> Details(int id)
        {
            var result = await _iAcomodacaoApp.FindOneAsync(id);

            // Configura as opções de serialização
            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                MaxDepth = 64 // Ajuste a profundidade conforme necessário
            };

            // Serializa o objeto para JSON
            var json = JsonSerializer.Serialize(result, jsonOptions);

            // Retorna o JSON serializado
            return Ok(json);
        }

    }
}
