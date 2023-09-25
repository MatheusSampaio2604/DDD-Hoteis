using Application.Interfaces;
using Application.ViewModel;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Linq;
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


        [HttpGet("ind")]
        // GET: TarifasController
        public async Task<ActionResult> Index()
        {
            var i = await _iTarifasApp.FindAllAsync();
            if (i.Count() > 0)
                return Ok(true);
            else
                return BadRequest();
        }

        [HttpGet("det")]
        // GET: TarifasController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var details = await _iTarifasApp.FindOneAsync(id);
            if (details is not null)
                return Ok(true);
            else
                return BadRequest();
        }

        // POST: TarifasController/Create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TarifasViewModel tarifasViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            tarifasViewModel.Nome = tarifasViewModel.Nome.ToUpper();

            var create = await _iTarifasApp.CreateAsync(tarifasViewModel);

            if (create is null)
                return BadRequest();
            else
                return Ok(true);

        }

        [HttpGet("edt")]
        // GET: TarifasController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var i = await _iTarifasApp.FindOneAsync(id);
            if (i is not null)
                return Ok(true);
            else
                return BadRequest();
        }

        // POST: TarifasController/Edit/5
        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TarifasViewModel tarifasViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            tarifasViewModel.Nome = tarifasViewModel.Nome.ToUpper();

            var edit = await _iTarifasApp.EditAsync(tarifasViewModel);

            if (edit is null)
            {
                return BadRequest();
            }
            else
                return Ok(true);

        }

        // POST: TarifasController/Delete/5
        [HttpPost("delete")]
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
                return BadRequest("Error");
            }
        }
    }
}
