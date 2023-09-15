using Application.Interfaces;
using Application.ViewModel;
using Domain.Interfaces;
using Domain.Models;
using Infra.Context;
using Infra.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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


        // GET: TarifasController
        public async Task<ActionResult> Index()
        {
            return View(await _iTarifasApp.FindAllAsync());
        }

        // GET: TarifasController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var details = await _iTarifasApp.FindOneAsync(id);
            return View(details);
        }

        // GET: TarifasController/Create
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
            tarifasViewModel.Valor = decimal.Parse(tarifasViewModel.Valor.ToString(), CultureInfo.InvariantCulture);

            var create = await _iTarifasApp.CreateAsync(tarifasViewModel);

            if (create is null)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: TarifasController/Edit/5
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

        // GET: TarifasController/Delete/5
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
