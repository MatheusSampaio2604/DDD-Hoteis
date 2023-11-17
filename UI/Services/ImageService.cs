using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Models;
using Application.ViewModel;
using System.Collections.Generic;

namespace UI.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
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
    }
}