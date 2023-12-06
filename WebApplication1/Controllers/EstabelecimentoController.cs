using Application.Interfaces;
using Application.ViewModel;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstabelecimentoController : ControllerBase
    {
        private readonly IHomeApp _IHomeApp;

        public EstabelecimentoController(IHomeApp iHomeApp)
        {
            _IHomeApp = iHomeApp;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> Index()
        {
            return Ok(new JsonResult(await _IHomeApp.FindAllAsync()));
        }

        [HttpGet("Detalhes")]
        public async Task<ActionResult> Details(int id)
        {
            return Ok(new JsonResult(await _IHomeApp.FindOneAsync(id)));
        }

        [HttpGet("Criar")]
        public ActionResult Create()
        {
            return Ok();
        }

        [HttpPost("Criar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HomeViewModel homeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new JsonResult(homeViewModel));
            }

            homeViewModel.Nome = homeViewModel.Nome.ToUpper();

            Home create = await _IHomeApp.CreateAsync(homeViewModel);

            if (create is null)
            {
                return Ok(new JsonResult("Error"));
            }
            return RedirectToAction(nameof(Index));

        }

        [HttpGet("Editar")]
        public async Task<ActionResult> Edit(int id)
        {
            return Ok(new JsonResult(await _IHomeApp.FindOneAsync(id)));
        }

        // POST: EstabelecimentoController/Edit/5
        [HttpPost("Editar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, HomeViewModel homeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new JsonResult(homeViewModel));
            }

            homeViewModel.Nome = homeViewModel.Nome.ToUpper();

            HomeViewModel edit = await _IHomeApp.EditAsync(homeViewModel);

            if (edit is null)
            {
                return Ok(new JsonResult("Error"));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Remover")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(new JsonResult(await _IHomeApp.FindOneAsync(id)));
        }

        // POST: EstabelecimentoController/Delete/5
        [HttpPost("Remover")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Home home)
        {
            try
            {
                _IHomeApp.Remove(home);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return Ok(new JsonResult("Error"));
            }
        }
    }
}
