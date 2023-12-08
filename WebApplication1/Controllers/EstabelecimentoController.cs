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
            try
            {
                return Ok(await _IHomeApp.FindAllAsync());
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("Detalhes")]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                return Ok(await _IHomeApp.FindOneAsync(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
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
                return Ok(homeViewModel);
            }
            try
            {
                homeViewModel.Nome = homeViewModel.Nome.ToUpper();

                return Ok(await _IHomeApp.CreateAsync(homeViewModel));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }



        }

        [HttpGet("Editar")]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                return Ok(await _IHomeApp.FindOneAsync(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        // POST: EstabelecimentoController/Edit/5
        [HttpPost("Editar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, HomeViewModel homeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Ok(homeViewModel);
            }

            try
            {
                homeViewModel.Nome = homeViewModel.Nome.ToUpper();

                return Ok(await _IHomeApp.EditAsync(homeViewModel));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            };

        }

        [HttpGet("Remover")]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(await _IHomeApp.FindOneAsync(id));
        }

        // POST: EstabelecimentoController/Delete/5
        [HttpPost("Remover")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Home home)
        {
            try
            {
                return Ok(_IHomeApp.Remove(home));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex);
            }
        }
    }
}
