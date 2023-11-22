using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Models;
using Application.ViewModel;
using System.Collections.Generic;
using Application.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace UI.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImagensApp _IImagensApp;

        public ImageService(IWebHostEnvironment webHostEnvironment, IImagensApp imagensApp)
        {
            _webHostEnvironment = webHostEnvironment;
            _IImagensApp = imagensApp;
        }

        public async Task<List<string>> SalvarImagensAsync(List<IFormFile> imagens, string nomeAcomodacao)
        {
            List<string> caminhosImagens = new();

            foreach (var imagem in imagens)
            {
                if (imagem == null || imagem.Length == 0)
                {
                    caminhosImagens = null;
                    continue;
                }

                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "img", nomeAcomodacao.Replace(" ", "_"));
                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                string fileName = Path.GetFileName(imagem.FileName);
                string filePath = Path.Combine(uploadDir, fileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imagem.CopyToAsync(fileStream);
                }

                caminhosImagens.Add(Path.Combine("img", nomeAcomodacao.Replace(" ", "_"), fileName).Replace("\\", "//"));
            }

            return caminhosImagens;
        }

        public async Task<Imagens> CreateAsync(ImagensViewModel uploadImagem)
        {
            Imagens i = await _IImagensApp.CreateAsync(uploadImagem);

            if (i != null)
                return i;
            else
                return null;
        }

        public async Task<List<ImagensViewModel>> FindImages(int id)
        {
            List<ImagensViewModel> img = await _IImagensApp.FindOneAsyncFindImageFromAcomodationID(id);
            return img;
        }

        public async Task ExcluirImagensSelecionadas(List<int> imagensParaExcluir)
        {
            if (imagensParaExcluir != null && imagensParaExcluir.Any())
            {
                foreach (var imagemId in imagensParaExcluir)
                {
                    var imagemParaExcluir = await _IImagensApp.FindOneAsync(imagemId);
                    if (imagemParaExcluir != null)
                    {
                        var imagemExcluir = new Imagens
                        {
                            Id = imagemParaExcluir.Id,
                            Nome = imagemParaExcluir.Nome,
                            Id_Acomodacao = imagemParaExcluir.Id_Acomodacao,
                            RotaImagem = imagemParaExcluir.RotaImagem,
                        };

                        string rota = _webHostEnvironment.WebRootPath + "//" + imagemExcluir.RotaImagem;
                        if (File.Exists(rota))
                            File.Delete(rota);

                        await _IImagensApp.Remove(imagemExcluir);
                    }
                }
            }
        }
    }
}