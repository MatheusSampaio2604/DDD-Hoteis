using Application.Interfaces;
using Application.ViewModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
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
            Utils utils = new Utils();

            // Retorna a suíte
            return Ok(await _iAcomodacaoApp.FindOneAsync(id));
        }










    }
}
