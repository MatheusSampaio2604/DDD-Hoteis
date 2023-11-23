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
        // private readonly IImagensApp _IImagensApp;
        private readonly ITarifasApp _ITarifasApp;
        private readonly IHomeApp _IHomeApp;


        private readonly ImageService _imageService;
        private readonly IWebHostEnvironment _Environment;

        public AcomodacaoController(
            IAcomodacaoApp iAcomodacaoApp
            , ITarifasApp iTarifasApp
            , IHomeApp iHomeApp
            , IWebHostEnvironment environment
            , ImageService imageService
            )
        {
            _IAcomodacaoApp = iAcomodacaoApp;
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

        [HttpGet("")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<AcomodacaoViewModel> item = await _IAcomodacaoApp.FindAllAsync();

            return View(item);
        }

        [HttpGet("Detalhes")]
        public async Task<ActionResult> Details(int id)
        {
            AcomodacaoViewModel details = await _IAcomodacaoApp.FindOneAsync(id);
            return View(details);
        }

        [HttpGet("Criar")]
        public async Task<ActionResult> Create()
        {
            ViewBag.Tarifas = await GetActiveTarifasAsync() ?? null;
            ViewBag.Home = await GetHomeAsync() ?? null;
            return View();
        }

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

            if (acomodacaoViewModel.Nome.Contains("chale", StringComparison.OrdinalIgnoreCase) || acomodacaoViewModel.Nome.Contains("suite", StringComparison.OrdinalIgnoreCase))
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
                    return View(acomodacaoViewModel);
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
                                var insertImages = await _imageService.CreateAsync(uploadImagem);
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
            var edit = await _IAcomodacaoApp.FindOneAsync(id);

            ViewBag.Tarifas = await GetActiveTarifasAsync() ?? null;
            ViewBag.Home = await GetHomeAsync() ?? null;

            return View(edit);
        }


        [HttpPost("Editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AcomodacaoViewModel acomodacaoViewModel)
        {
            try
            {
                var oldData = await _IAcomodacaoApp.FindNoTrackinOneAsync(acomodacaoViewModel.Id);

                if (oldData.IdHome != acomodacaoViewModel.IdHome ||
                oldData.IdValor != acomodacaoViewModel.IdValor ||
                    oldData.Ativo != acomodacaoViewModel.Ativo ||
                    oldData.Descricao != acomodacaoViewModel.Descricao)
                {
                    var edit = await _IAcomodacaoApp.EditAsync(acomodacaoViewModel);
                }

                if (acomodacaoViewModel.Fotos != null && acomodacaoViewModel.Fotos.Count > 0)
                {
                    var novosCaminhosImagens = await _imageService.SalvarImagensAsync(acomodacaoViewModel.Fotos, acomodacaoViewModel.Nome);
                    if (novosCaminhosImagens != null)
                    {
                        var antigosCaminhosImagens = await _imageService.FindImages(acomodacaoViewModel.Id);

                        foreach (var novoCaminho in novosCaminhosImagens)
                        {
                            if (!antigosCaminhosImagens.Any(img => img.RotaImagem == novoCaminho))
                            {
                                ImagensViewModel uploadImagem = new()
                                {
                                    Id_Acomodacao = acomodacaoViewModel.Id,
                                    RotaImagem = novoCaminho,
                                    Nome = acomodacaoViewModel.Nome,
                                };
                                var insertImages = await _imageService.CreateAsync(uploadImagem);
                            }
                            else if (antigosCaminhosImagens.Any(img => img.RotaImagem == novoCaminho))
                            {
                                TempData["MensagemAlerta"] = $"A imagem com o caminho já existe.";

                                ViewBag.Tarifas = await GetActiveTarifasAsync() ?? null;
                                ViewBag.Home = await GetHomeAsync() ?? null;
                                return View(acomodacaoViewModel);
                            }

                        }
                    }
                }

                if (acomodacaoViewModel.ImagensExcluir != null)
                    await _imageService.ExcluirImagensSelecionadas(acomodacaoViewModel.ImagensExcluir);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao Editar a entidade: " + ex.Message);
            }
        }




        // [HttpGet("Remover")]
        // public async Task<ActionResult> Delete(int id)
        // {
        //     var acomodacao = await _IAcomodacaoApp.FindOneAsync(id);

        //     if (acomodacao == null)
        //     {
        //         return NotFound(); // Ou uma View de erro específica para item não encontrado
        //     }

        //     return View(acomodacao);
        // }

        // [HttpPost("Remover")]
        // [ValidateAntiForgeryToken]
        // public async Task<ActionResult> Delete([Bind("Id")] Acomodacao acomodacao)
        // {
        //     try
        //     {
        //         await _IAcomodacaoApp.Remove(acomodacao);
        //         return RedirectToAction("Index");
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"Erro ao remover entidade: {ex.Message}");
        //         return View("Error");
        //     }
        // }

    }
}

