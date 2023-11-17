using Application.Interfaces;
using Application.ViewModel;
using Domain.Interfaces;
using Domain.Models;
using Infra.Context;
using Infra.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UI.Services;

namespace UI.Controllers
{
    [Route("Admin/[controller]")]
    public class AcomodacaoController : Controller
    {
        private readonly IAcomodacaoApp _IAcomodacaoApp;
        private readonly IImagensApp _IImagensApp;
        private readonly ITarifasApp _ITarifasApp;
        private readonly IHomeApp _IHomeApp;


        private readonly ImageService _imageService;
        private readonly IWebHostEnvironment _Environment;

        public AcomodacaoController(
            IAcomodacaoApp iAcomodacaoApp
            , IImagensApp imagensApp
            , ITarifasApp iTarifasApp
            , IHomeApp iHomeApp
            , IWebHostEnvironment environment
            , ImageService imageService
            )
        {
            _IAcomodacaoApp = iAcomodacaoApp;
            _IImagensApp = imagensApp;
            _ITarifasApp = iTarifasApp;
            _IHomeApp = iHomeApp;
            _Environment = environment;
            _imageService = imageService;
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
                    if (acomodacaoViewModel.Fotos != null && acomodacaoViewModel.Fotos.Count > 0)
                    {
                        var caminhosImagens = await _imageService.SalvarImagensAsync(acomodacaoViewModel.Fotos, acomodacaoViewModel.Nome);

                        if (caminhosImagens != null)
                        {
                            foreach (var item in caminhosImagens)
                            {
                                ImagensViewModel uploadImagem = new()
                                {
                                    Id_Acomodacao = create.Id,
                                    Nome = create.Nome,
                                    RotaImagem = item,
                                };
                                var createUploadImagem = await _IImagensApp.CreateAsync(uploadImagem);
                            }
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
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
            try
            {
                
                if (acomodacaoViewModel.Fotos != null && acomodacaoViewModel.Fotos.Count > 0)
                {
                    var novosCaminhosImagens = await _imageService.SalvarImagensAsync(acomodacaoViewModel.Fotos, acomodacaoViewModel.Nome);

                    foreach (var item in novosCaminhosImagens)
                    {
                        ImagensViewModel uploadImagem = new()
                        {
                            // Id = ,
                            Id_Acomodacao = acomodacaoViewModel.Id,
                            RotaImagem = item,
                        };
                        var createUploadImagem = await _IImagensApp.EditAsync(uploadImagem);
                    }
                }

                var edit = await _IAcomodacaoApp.EditAsync(acomodacaoViewModel);

                if (edit is null)
                {
                    ViewBag.Tarifas = await GetActiveTarifasAsync() ?? null;
                    ViewBag.Home = await GetHomeAsync() ?? null;
                    return View("Error");
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw new Exception($"Ocorreu um erro ao Editar a entidade. {ex.Message}");
            }
        }




        [HttpGet("Remover")]
        public async Task<ActionResult> Delete(int id)
        {
            var acomodacao = await _IAcomodacaoApp.FindOneAsync(id);
            if (acomodacao == null)
            {
                return NotFound(); // Ou uma View de erro específica para item não encontrado
            }

            return View(acomodacao);
        }

        [HttpPost("Remover")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind("Id")] Acomodacao acomodacao)
        {
            try
            {
                await _IAcomodacaoApp.Remove(acomodacao);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao remover entidade: {ex.Message}");
                return View("Error");
            }
        }

    }
}

/* // Obtenha o caminho do arquivo a ser removido.
                var caminhoArquivo = Path.Combine(_Environment.WebRootPath, acomodacaoViewModel.RotaImagem);

                // Verifique se o arquivo existe antes de tentar removê-lo.
                if (System.IO.File.Exists(caminhoArquivo))
                {
                    // Remova o arquivo do sistema de arquivos.
                    System.IO.File.Delete(caminhoArquivo);
                }

                // Converter ViewModel para Model
                Acomodacao acomodacaoModel = new Acomodacao
                {
                
                    Id = acomodacaoViewModel.Id,
                    Nome = acomodacaoViewModel.Nome,
                    Ativo = acomodacaoViewModel.Ativo,
                    Descricao = acomodacaoViewModel.Descricao,
                    RotaImagem = acomodacaoViewModel.RotaImagem,
                    IdHome = acomodacaoViewModel.IdHome,
                    IdValor = acomodacaoViewModel.IdValor,
                    
                };**/