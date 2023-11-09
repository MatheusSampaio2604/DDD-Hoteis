using Application.Interfaces;
using Application.ViewModel;
using Domain.Interfaces;
using Domain.Models;
using Infra.Context;
using Infra.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UI.Controllers
{
    [Route("Admin/[controller]")]
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

        public async Task<IEnumerable<TarifasViewModel>> GetActiveTarifasAsync()
        {
            var tarifas = await _ITarifasApp.FindAllAsync();
            return tarifas.Where(t => t.Ativo).ToList();
        }

        public async Task<IEnumerable<HomeViewModel>> GetHomeAsync()
        {
            return await _IHomeApp.FindAllAsync();
        }

        // GET: AplicacaoasController
        [HttpGet("")]
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
            ViewBag.Tarifas = await GetActiveTarifasAsync() ?? null;
            ViewBag.Home = await GetHomeAsync() ?? null;
            return View();
        }

        // POST: AplicacaoasController/Create
        [HttpPost("Criar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AcomodacaoViewModel acomodacaoViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Tarifas = await GetActiveTarifasAsync() ?? null;
                ViewBag.Home = await GetHomeAsync() ?? null;
                return View(acomodacaoViewModel);
            }

            if (acomodacaoViewModel.Nome.Contains("chale", StringComparison.OrdinalIgnoreCase) ||
                    acomodacaoViewModel.Nome.Contains("suite", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Nome", "O campo Nome não pode conter 'Chalé' ou 'Suíte'!");
                ViewBag.Tarifas = await GetActiveTarifasAsync() ?? null;
                ViewBag.Home = await GetHomeAsync() ?? null;
                return View(acomodacaoViewModel);
            }

            acomodacaoViewModel.Nome = acomodacaoViewModel.TipoAcomodacao + " " + acomodacaoViewModel.Nome;
            acomodacaoViewModel.Nome = acomodacaoViewModel.Nome.ToUpper();

            //if (acomodacaoViewModel.Fotos is not null)
            //{
            //    await CriarComImagem(acomodacaoViewModel);
            //}
            //else
            //
            var exist = await _IAcomodacaoApp.FindAllAsync();

            if (exist.Any(x => x.Nome == acomodacaoViewModel.Nome))
                return Unauthorized("Já existe ");
            else
            {
                var create = await _IAcomodacaoApp.CreateAsync(acomodacaoViewModel);

                if (create is null)
                {
                    ViewBag.Tarifas = await GetActiveTarifasAsync() ?? null;
                    ViewBag.Home = await GetHomeAsync() ?? null;
                    return View("Create", acomodacaoViewModel);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            //}
            //return RedirectToAction(nameof(Index));
        }


        [HttpGet("Editar")]
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Tarifas = await GetActiveTarifasAsync() ?? null;
            ViewBag.Home = await GetHomeAsync() ?? null;
            return View(await _IAcomodacaoApp.FindOneAsync(id));
        }


        [HttpPost("Editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AcomodacaoViewModel acomodacaoViewModel)
        {

            acomodacaoViewModel.Nome = acomodacaoViewModel.Nome.ToUpper();

            // Verifique se o usuário deseja atualizar a imagem
            if (acomodacaoViewModel.Fotos is not null)
            {
                await CriarComImagem(acomodacaoViewModel);
            }
            //           else
            //         {
            // O usuário não deseja atualizar a imagem, mantenha a imagem existente.
            //var acomodacaoExistente = await _IAcomodacaoApp.FindNoTrackinOneAsync(acomodacaoViewModel.Id);

            // Mantenha a imagem existente, se houver.
            //                acomodacaoViewModel.RotaImagem = acomodacaoExistente.RotaImagem;
            //            }


            var edit = await _IAcomodacaoApp.EditAsync(acomodacaoViewModel);

            if (edit is null)
            {
                ViewBag.Tarifas = await GetActiveTarifasAsync() ?? null;
                ViewBag.Home = await GetHomeAsync() ?? null;
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet("Remover")]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _IAcomodacaoApp.FindOneAsync(id));
        }

        [HttpPost("Remover")]
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
                var nomePasta = acomodacaoViewModel.Nome.Replace(" ", "_"); // Remova espaços no nome da acomodação
                var pastaDestino = Path.Combine(wwwroot, "img", nomePasta);

                // Verifique se a pasta de destino já existe. Se não, crie-a.
                if (!Directory.Exists(pastaDestino))
                {
                    Directory.CreateDirectory(pastaDestino);
                }

                var tipoArquivo = Path.GetExtension(acomodacaoViewModel.Fotos.FileName);
                var nomeArquivo = string.Concat(acomodacaoViewModel.Fotos.FileName.ToUpper(), tipoArquivo);
                var diretorioArquivoSalvar = Path.Combine(pastaDestino, nomeArquivo);

                var stream = new FileStream(diretorioArquivoSalvar, FileMode.Create);
                await acomodacaoViewModel.Fotos.CopyToAsync(stream);

                acomodacaoViewModel.RotaImagem = $"https://localhost:5001/img/{nomePasta}/{nomeArquivo}";

                await _IAcomodacaoApp.CreateAsync(acomodacaoViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // Lide com erros apropriadamente, como registrar ou lançar uma exceção.
            }
        }

    }
}
