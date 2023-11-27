using Application.Interfaces;
using Application.ViewModel;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Services;

namespace UI.Controllers
{
    [Route("[controller]")]
    public class AcomodacaoController : Controller
    {
        private readonly IAcomodacaoApp _IAcomodacaoApp;
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
            IEnumerable<TarifasViewModel> tarifas = await _ITarifasApp.FindAllAsync();
            return tarifas.Where(t => t.Ativo).ToList();
        }

        public async Task<IEnumerable<HomeViewModel>> GetActiveHomeAsync()
        {
            return await _IHomeApp.FindAllAsync();
        }

        private async Task<ActionResult> ReturnToScreenMode(AcomodacaoViewModel acomodacaoViewModel, string viewName)
        {
            ViewBag.Tarifas = await GetActiveTarifasAsync() ?? null;
            ViewBag.Home = await GetActiveHomeAsync() ?? null;
            return View(viewName, acomodacaoViewModel);
        }

        private static bool ErroVerificarSubstring(string[] substringsProibidas, string nomeAcomodacao)
        {
            return substringsProibidas.Any(substring => nomeAcomodacao.Contains(substring, StringComparison.OrdinalIgnoreCase));
        }

        private static bool DeveRealizarEdicao(AcomodacaoViewModel oldData, AcomodacaoViewModel newData)
        {
            return oldData.IdHome != newData.IdHome ||
                   oldData.IdValor != newData.IdValor ||
                   oldData.Ativo != newData.Ativo ||
                   oldData.Descricao != newData.Descricao;
        }

        private async Task ProcessarImagensAsync(AcomodacaoViewModel acomodacaoViewModel, int? id)
        {
            if (acomodacaoViewModel.Fotos?.Count > 0)
            {
                IEnumerable<string> novosCaminhosImagens;

                if (acomodacaoViewModel.Id != 0) // Se Id  tiver valor, é o cenário de Edição
                {
                    novosCaminhosImagens = await _imageService.AddImagesAsync(acomodacaoViewModel.Fotos, acomodacaoViewModel.Nome);

                    if (novosCaminhosImagens != null)
                    {
                        List<ImagensViewModel> antigosCaminhosImagens = await _imageService.FindImagesById(acomodacaoViewModel.Id);

                        List<ImagensViewModel> imagensParaAdicionar = novosCaminhosImagens
                            .Where(novoCaminho => !antigosCaminhosImagens.Any(img => img.RotaImagem == novoCaminho))
                            .Select(novoCaminho => new ImagensViewModel
                            {
                                Id_Acomodacao = acomodacaoViewModel.Id,
                                RotaImagem = novoCaminho,
                                Nome = acomodacaoViewModel.Nome,
                            }).ToList();

                        await _imageService.CreateManyAsync(imagensParaAdicionar);

                        IEnumerable<string> caminhosImagensExistentes = imagensParaAdicionar.Select(img => img.RotaImagem);
                        IEnumerable<string> imagensDuplicadas = novosCaminhosImagens.Except(caminhosImagensExistentes);

                        if (imagensDuplicadas.Any())
                        {
                            TempData["MensagemAlerta"] = $"Algumas imagens com os caminhos já existem: {string.Join(", ", imagensDuplicadas)}";
                            await ReturnToScreenMode(acomodacaoViewModel, "Edit");
                            return;
                        }
                    }
                }
                else // Se Id não tiver valor, é o cenário de Criação
                {
                    novosCaminhosImagens = await _imageService.AddImagesAsync(acomodacaoViewModel.Fotos, acomodacaoViewModel.Nome);

                    if (novosCaminhosImagens != null)
                    {
                        List<ImagensViewModel> imagensParaAdicionar = novosCaminhosImagens
                            .Select(item => new ImagensViewModel
                            {
                                Id_Acomodacao = id,
                                Nome = acomodacaoViewModel.Nome,
                                RotaImagem = item,
                            }).ToList();

                        await _imageService.CreateManyAsync(imagensParaAdicionar);
                    }
                }
            }

            if (acomodacaoViewModel.ImagensExcluir != null)
            {
                await _imageService.RemoveImagesByIDFromDB(acomodacaoViewModel.ImagensExcluir);
            }
        }

        private static void MapImagesIds(List<ImagensViewModel> imagens, List<int> imageIds)
        {
            foreach (ImagensViewModel imagem in imagens)
            {
                imageIds.Add(imagem.Id);
            }
        }

        private async Task<int> RemoveAcomodacao(AcomodacaoViewModel removeData)
        {
            if (removeData != null)
            {
                Acomodacao acomodacao = new Acomodacao
                {
                    Id = removeData.Id,
                    Nome = removeData.Nome,
                    Descricao = removeData.Descricao,
                    Ativo = removeData.Ativo,
                    IdValor = removeData.IdValor,
                    IdHome = removeData.IdHome,
                };
                return await _IAcomodacaoApp.Remove(acomodacao);
            }
            return 0;
        }

        /*`````````````````````````````````````````````````````````````````````````````````````````````````````*/

        [HttpGet("")]
        public async Task<ActionResult> Index()
        {
            try { return View(await _IAcomodacaoApp.FindAllAsync()); }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Ocorreu um erro ao buscar a lista: " + ex.Message;
                return View("Error");
            }
        }

        [HttpGet("Detalhes")]
        public async Task<ActionResult> Details(int id)
        {
            try { return View(await _IAcomodacaoApp.FindOneAsync(id)); }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Ocorreu um erro ao buscar a entidade: " + ex.Message;
                return View("Error");
            }
        }

        [HttpGet("Criar")]
        public async Task<ActionResult> Create()
        {
            return await ReturnToScreenMode(new AcomodacaoViewModel(), "Create");
        }

        [HttpPost("Criar")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AcomodacaoViewModel acomodacaoViewModel)
        {
            string[] substringsProibidas = { "chale", "Chalé", "Suíte", "suite" };
            try
            {
                if (!ModelState.IsValid) return await ReturnToScreenMode(acomodacaoViewModel, "Create");
                if (ErroVerificarSubstring(substringsProibidas, acomodacaoViewModel.Nome))
                {
                    ModelState.AddModelError(nameof(acomodacaoViewModel.Nome), "*O campo não pode conter 'Chalé' ou 'Suíte'!");
                    if (acomodacaoViewModel.Fotos?.Count > 0)
                        ModelState.AddModelError(nameof(acomodacaoViewModel.Fotos), "*Insira Novamente!");

                    return await ReturnToScreenMode(acomodacaoViewModel, "Create");
                }

                acomodacaoViewModel.Nome = (acomodacaoViewModel.TipoAcomodacao + " " + acomodacaoViewModel.Nome).ToUpper();

                IEnumerable<AcomodacaoViewModel> exist = await _IAcomodacaoApp.FindAllAsync();

                if (exist.Any(x => x.Nome == acomodacaoViewModel.Nome)) return Unauthorized("Já existe ");

                Acomodacao create = await _IAcomodacaoApp.CreateAsync(acomodacaoViewModel);

                if (create is null) return await ReturnToScreenMode(acomodacaoViewModel, "Create");

                await ProcessarImagensAsync(acomodacaoViewModel, create.Id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Ocorreu um erro ao Criar a entidade: " + ex.Message;
                return View("Error");
            }
        }

        [HttpGet("Editar")]
        public async Task<ActionResult> Edit(int id)
        {
            try { return await ReturnToScreenMode(await _IAcomodacaoApp.FindOneAsync(id), "Edit"); }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Ocorreu um erro ao buscar a entidade: " + ex.Message;
                return View("Error");
            }
        }


        [HttpPost("Editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AcomodacaoViewModel acomodacaoViewModel)
        {
            try
            {
                AcomodacaoViewModel oldData = await _IAcomodacaoApp.FindNoTrackinOneAsync(acomodacaoViewModel.Id);

                if (DeveRealizarEdicao(oldData, acomodacaoViewModel)) await _IAcomodacaoApp.EditAsync(acomodacaoViewModel);

                await ProcessarImagensAsync(acomodacaoViewModel, null);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Ocorreu um erro ao Editar a entidade: " + ex.Message;
                return await ReturnToScreenMode(acomodacaoViewModel, "Edit");
            }
        }


        [HttpGet("Remover")]
        public async Task<ActionResult> Delete(int id)
        {
            try { return View(await _IAcomodacaoApp.FindOneAsync(id)); }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = "Ocorreu um erro ao buscar a entidade: " + ex.Message;
                return View("Error");
            }
        }

        [HttpPost("Remover")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind("Id")] AcomodacaoViewModel acomodacaoViewModel)
        {
            try
            {
                List<int> imgBD = new();

                AcomodacaoViewModel removeData = await _IAcomodacaoApp.FindOneAsync(acomodacaoViewModel.Id);
                List<ImagensViewModel> img = await _imageService.FindImagesById(acomodacaoViewModel.Id);

                MapImagesIds(img, imgBD);

                bool remocaoBDImages = await _imageService.RemoveImagesByIDFromDB(imgBD);
                bool remocaoDiretoryImages = _imageService.RemoveDirectory(img);

                if (!remocaoDiretoryImages)
                {
                    TempData["MensagemErro"] = "Erro ao remover o diretório das imagens.";
                    return RedirectToAction("Delete");
                }

                int removeFromBD = await RemoveAcomodacao(removeData);

                if (removeFromBD is not 0) return View("Error");

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

