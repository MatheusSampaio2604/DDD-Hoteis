﻿using Application.Interfaces;
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
            var i = await _iAcomodacaoApp.FindAllAsync();

            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                MaxDepth = 64 
            };

            var json = JsonSerializer.Serialize(i, jsonOptions);

            return Ok(json);
        }

        [HttpGet("Detalhes")]
        public async Task<ActionResult> Details(int id)
        {
            var result = await _iAcomodacaoApp.FindOneAsync(id);

            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                MaxDepth = 64,
            };

            var json = JsonSerializer.Serialize(result, jsonOptions);
            return Ok(json);
        }
    }
}