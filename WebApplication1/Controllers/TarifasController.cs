﻿using Application.Interfaces;
using Application.ViewModel;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarifasController : ControllerBase
    {
        private readonly ITarifasApp _iTarifasApp;

        public TarifasController(ITarifasApp iTarifasApp)
        {
            _iTarifasApp = iTarifasApp;
        }


        [HttpGet("GetAll")]
        // GET: TarifasController
        public async Task<ActionResult> Index()
        {
            //Utils utils = new();
            System.Collections.Generic.IEnumerable<TarifasViewModel> i = await _iTarifasApp.FindAllAsync();
            // Configura as opções de serialização
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                MaxDepth = 64 // Ajuste a profundidade conforme necessário
            };

            // Serializa o objeto para JSON
            string json = JsonSerializer.Serialize(i, jsonOptions);

            // Retorna o JSON serializado
            return Ok(json);
        }


        [HttpGet("Details")]
        // GET: TarifasController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            TarifasViewModel i = await _iTarifasApp.FindOneAsync(id);
            // Configura as opções de serialização
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                MaxDepth = 64 // Ajuste a profundidade conforme necessário
            };

            // Serializa o objeto para JSON
            string json = JsonSerializer.Serialize(i, jsonOptions);

            // Retorna o JSON serializado
            return Ok(json);

        }

        // POST: TarifasController/Create
        [HttpPost("Tarifas/Criar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TarifasViewModel tarifasViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            tarifasViewModel.Nome = tarifasViewModel.Nome.ToUpper();

            Tarifas create = await _iTarifasApp.CreateAsync(tarifasViewModel);

            if (create is null)
                return BadRequest();
            else
                return Ok(true);

        }

        [HttpGet("Edit")]
        // GET: TarifasController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            TarifasViewModel i = await _iTarifasApp.FindOneAsync(id);
            if (i is not null)
                return Ok(true);
            else
                return BadRequest();
        }

        // POST: TarifasController/Edit/5
        [HttpPost("Tarifas/Editar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TarifasViewModel tarifasViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            tarifasViewModel.Nome = tarifasViewModel.Nome.ToUpper();

            TarifasViewModel edit = await _iTarifasApp.EditAsync(tarifasViewModel);

            if (edit is null)
            {
                return BadRequest();
            }
            else
                return Ok(true);

        }

        // POST: TarifasController/Delete/5
        [HttpPost("Tarifas/Remover")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tarifas tarifas)
        {
            try
            {
                _iTarifasApp.Remove(tarifas);

                return Ok(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest("Error");
            }
        }
    }
}
