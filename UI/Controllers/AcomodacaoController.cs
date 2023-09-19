using Application.Interfaces;
using Application.ViewModel;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class AcomodacaoController : Controller
    {
        private readonly IAcomodacaoApp _IAcomodacaoApp;
        private readonly ITarifasApp _ITarifasApp;
        private readonly IHomeApp _IHomeApp;

        private readonly IWebHostEnvironment _Environment;

        public AcomodacaoController(
            IAcomodacaoApp iAcomodacaoApp
            , ITarifasApp iTarifasApp
            , IHomeApp iHomeApp
            , IWebHostEnvironment environment)
        {
            _IAcomodacaoApp = iAcomodacaoApp;
            _ITarifasApp = iTarifasApp;
            _IHomeApp = iHomeApp;
            _Environment = environment;
        }


        // GET: AplicacaoasController
        public async Task<ActionResult> Index()
        {

            IEnumerable<AcomodacaoViewModel> item = await _IAcomodacaoApp.FindAllAsync();
            return View(item);
        }

        // GET: AplicacaoasController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var details = await _IAcomodacaoApp.FindOneAsync(id);
            return View(details);
        }

        // GET: AplicacaoasController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Tarifas = (await _ITarifasApp.FindAllAsync()) ?? null;
            ViewBag.Home = (await _IHomeApp.FindAllAsync()) ?? null;
            return View();
        }

        // POST: AplicacaoasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AcomodacaoViewModel acomodacaoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(acomodacaoViewModel);
            }

            acomodacaoViewModel.Nome = acomodacaoViewModel.Nome.ToUpper();

            var create = await _IAcomodacaoApp.CreateAsync(acomodacaoViewModel);

            if (create is null)
            {
                return View("Create", acomodacaoViewModel);
            }
            else
            {
                //if(acomodacaoViewModel.Fotos is not null)
                //{
                //    // await SalvarImagemProduto(acomodacaoViewModel);
                //}

                return RedirectToAction(nameof(Index));
            }


        }

        // GET: AplicacaoasController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Tarifas = (await _ITarifasApp.FindAllAsync()) ?? null;
            ViewBag.Home = (await _IHomeApp.FindAllAsync()) ?? null;
            return View(await _IAcomodacaoApp.FindOneAsync(id));
        }

        // POST: AplicacaoasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AcomodacaoViewModel acomodacaoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(acomodacaoViewModel);
            }

            acomodacaoViewModel.Nome = acomodacaoViewModel.Nome.ToUpper();
            //if (acomodacaoViewModel.Fotos is not null)
            //{
            //    await SalvarImagemProduto(acomodacaoViewModel);
            //}
            //else
            //{
            //    //var oldProduto = await _IAcomodacaoApp.FindNoTrackinOneAsync(acomodacaoViewModel.Id);

            //    //acomodacaoViewModel.RotaImagem = oldProduto.RotaImagem ?? null;

            var edit = await _IAcomodacaoApp.EditAsync(acomodacaoViewModel);

            if (edit is null)
            {
                return View("Error");
            }
            //}

            return RedirectToAction(nameof(Index));
        }

        // GET: AplicacaoasController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _IAcomodacaoApp.FindOneAsync(id));
        }

        // POST: AplicacaoasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Acomodacao acomodacao)
        {
            try
            {
                _IAcomodacaoApp.Remove(acomodacao);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        //public async Task SalvarImagemProduto(AcomodacaoViewModel acomodacaoViewModel)
        //{
        //    if (acomodacaoViewModel.Fotos is not null)
        //    {
        //        try
        //        {
        //            var oldProduto = await _IAcomodacaoApp.FindOneAsync(acomodacaoViewModel.Id);

        //            var webRoot = _Environment.WebRootPath;
        //            var extension = Path.GetExtension(acomodacaoViewModel.Fotos.FileName);
        //            var nomeArquivo = string.Concat(acomodacaoViewModel.Nome.ToString(), extension);
        //            var diretorioArquivoSalvar = Path.Combine(webRoot, "img", nomeArquivo);

        //            var stream = new FileStream(diretorioArquivoSalvar, FileMode.Create);
        //            await acomodacaoViewModel.Fotos.CopyToAsync(stream);


        //            acomodacaoViewModel.RotaImagem = $"https://localhost:5001/imgProdutos/{nomeArquivo}";

        //            await _IAcomodacaoApp.EditAsync(acomodacaoViewModel);

        //        }
        //        catch (Exception ex)
        //        {
        //            return;
        //        }
        //    }


        //}

    }
}
