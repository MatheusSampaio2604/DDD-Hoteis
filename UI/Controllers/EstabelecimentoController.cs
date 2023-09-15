using Application.Interfaces;
using Application.ViewModel;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class EstabelecimentoController : Controller
    {
        private readonly IHomeApp _IHomeApp;

        public EstabelecimentoController(IHomeApp iHomeApp)
        {
            _IHomeApp = iHomeApp;
        }
        // GET: EstabelecimentoController
        public async Task<ActionResult> Index()
        {
            return View(await _IHomeApp.FindAllAsync());
        }

        // GET: EstabelecimentoController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _IHomeApp.FindOneAsync(id));
        }

        // GET: EstabelecimentoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstabelecimentoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HomeViewModel homeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(homeViewModel);
            }

            homeViewModel.Nome = homeViewModel.Nome.ToUpper();

            var create = await _IHomeApp.CreateAsync(homeViewModel);

            if (create is null)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        
        }

        // GET: EstabelecimentoController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _IHomeApp.FindOneAsync(id));
        }

        // POST: EstabelecimentoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, HomeViewModel homeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(homeViewModel);
            }

            homeViewModel.Nome = homeViewModel.Nome.ToUpper();

            var edit = await _IHomeApp.EditAsync(homeViewModel);

            if (edit is null)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: EstabelecimentoController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _IHomeApp.FindOneAsync(id));
        }

        // POST: EstabelecimentoController/Delete/5
        [HttpPost]
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
                return View("Error");
            }
        }
    }
}
