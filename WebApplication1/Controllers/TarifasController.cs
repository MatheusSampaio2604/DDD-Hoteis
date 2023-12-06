using Application.Interfaces;
using Application.ViewModel;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
        public async Task<ActionResult> Index()
        {
            try
            {
                return Ok(new JsonResult(await _iTarifasApp.FindAllAsync()));
            }
            catch (Exception)
            {
                return NotFound("Não foi encontrado Valores");
            }
        }

        [HttpGet("Details")]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                return Ok(new JsonResult(await _iTarifasApp.FindOneAsync(id)));
            }
            catch (Exception)
            {
                return NotFound("Não foi encontrado Valores");
            }

        }

        [HttpGet("Criar")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPost("Criar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TarifasViewModel tarifasViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                tarifasViewModel.Nome = tarifasViewModel.Nome.ToUpper();

                return Ok(new JsonResult(await _iTarifasApp.CreateAsync(tarifasViewModel)));
            }
            catch (Exception)
            {
                return BadRequest("Não foi possivel completar a sua solicitação. \nTente novamente!");
            }
        }

        [HttpGet("Edit")]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                return Ok(new JsonResult(await _iTarifasApp.FindOneAsync(id)));
            }
            catch (Exception)
            {
                return BadRequest("Não foi possivel completar a sua solicitação. \nTente novamente!");
            }
        }

        [HttpPost("Tarifas/Editar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TarifasViewModel tarifasViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                tarifasViewModel.Nome = tarifasViewModel.Nome.ToUpper();

                return Ok(new JsonResult(await _iTarifasApp.EditAsync(tarifasViewModel)));
            }
            catch (Exception)
            {
                return BadRequest("Não foi possivel completar a sua solicitação. \nTente novamente!");
            }
        }

        [HttpGet("Remover")]
        public ActionResult Delete(int id)
        {
            try
            {
                return Ok(new JsonResult(_iTarifasApp.FindOneAsync(id)));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }


        [HttpPost("Remover")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tarifas tarifas)
        {
            try
            {
                return Ok(new JsonResult(_iTarifasApp.Remove(tarifas)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
