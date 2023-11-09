using Application.Interfaces;
using Application.ViewModel;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class TarifasController : Controller
    {
        private readonly ITarifasApp _iTarifasApp;

        public TarifasController(ITarifasApp iTarifasApp)
        {
            _iTarifasApp = iTarifasApp;
        }

        [HttpGet("Administrador/Tarifas")]
        public async Task<ActionResult> Index()
        {
            return View(await _iTarifasApp.FindAllAsync());
        }

        [HttpGet("Administrador/Tarifas/Detalhes")]
        public async Task<ActionResult> Details(int id)
        {
            var details = await _iTarifasApp.FindOneAsync(id);
            return View(details);
        }

        [HttpGet("Administrador/Tarifas/Criar")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TarifasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TarifasViewModel tarifasViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(tarifasViewModel);
            }

            tarifasViewModel.Nome = tarifasViewModel.Nome.ToUpper();

            var create = await _iTarifasApp.CreateAsync(tarifasViewModel);

            if (create is null)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));

        }

        [HttpGet("Administrador/Tarifas/Editar")]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _iTarifasApp.FindOneAsync(id));
        }

        // POST: TarifasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TarifasViewModel tarifasViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(tarifasViewModel);
            }

            tarifasViewModel.Nome = tarifasViewModel.Nome.ToUpper();

            var edit = await _iTarifasApp.EditAsync(tarifasViewModel);

            if (edit is null)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Administrador/Tarifas/Remover")]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _iTarifasApp.FindOneAsync(id));
        }

        // POST: TarifasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tarifas tarifas)
        {
            try
            {
                _iTarifasApp.Remove(tarifas);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}
