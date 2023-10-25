using Application.Interfaces;
using Application.ViewModel;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        [HttpGet("MostrarTudo")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<AcomodacaoViewModel> item = await _IAcomodacaoApp.FindAllAsync();

            return View(item);
        }

        // GET: AplicacaoasController/Details/5
        [HttpGet("Detalhes")]
        public async Task<ActionResult> Details(int id)
        {
            var details = await _IAcomodacaoApp.FindOneAsync(id);
            return View(details);
        }

        // GET: AplicacaoasController/Create
        [HttpGet("Criar")]
        public async Task<ActionResult> Create()
        {
            var tarifas = await _ITarifasApp.FindAllAsync();
            ViewBag.Tarifas = tarifas.Where(t => t.Ativo == true).ToList() ?? null;
            ViewBag.Home = await _IHomeApp.FindAllAsync() ?? null;
            return View();
        }

        // POST: AplicacaoasController/Create
        [HttpPost("Criacao")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AcomodacaoViewModel acomodacaoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(acomodacaoViewModel);
            }

            acomodacaoViewModel.Nome = acomodacaoViewModel.Nome.ToUpper();
            if (acomodacaoViewModel.Fotos is not null)
            {
                await CriarComImagem(acomodacaoViewModel);
            }
            else
            {
                var create = await _IAcomodacaoApp.CreateAsync(acomodacaoViewModel);

                if (create is null)
                {
                    return View("Create", acomodacaoViewModel);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: AplicacaoasController/Edit/5
        [HttpGet("Editar")]
        public async Task<ActionResult> Edit(int id)
        {
            var tarifas = await _ITarifasApp.FindAllAsync();
            ViewBag.Tarifas = tarifas.Where(t => t.Ativo == true).ToList() ?? null;
            ViewBag.Home = (await _IHomeApp.FindAllAsync()) ?? null;
            return View(await _IAcomodacaoApp.FindOneAsync(id));
        }

        // POST: AplicacaoasController/Edit/5
        [HttpPost("Edicao")]
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
        [HttpGet("Deletar")]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _IAcomodacaoApp.FindOneAsync(id));
        }

        // POST: AplicacaoasController/Delete/5
        [HttpPost("Delete")]
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

        public async Task CriarComImagem(AcomodacaoViewModel acomodacaoViewModel)
        {
            try
            {

                var wwwroot = _Environment.WebRootPath;
                var tipoArquivo = Path.GetExtension(acomodacaoViewModel.Fotos.FileName);
                var nomeArquivo = string.Concat(acomodacaoViewModel.Nome.ToString(), "_", acomodacaoViewModel.Fotos.FileName.ToUpper(), tipoArquivo);
                var diretorioArquivoSalvar = Path.Combine(wwwroot, "img", nomeArquivo);

                var stream = new FileStream(diretorioArquivoSalvar, FileMode.Create);
                await acomodacaoViewModel.Fotos.CopyToAsync(stream);

                acomodacaoViewModel.RotaImagem = $"https://localhost:5001/img/{nomeArquivo}";


                await _IAcomodacaoApp.CreateAsync(acomodacaoViewModel);


            }
            catch (Exception ex)
            {
                return;
            }
        }

    }
}
