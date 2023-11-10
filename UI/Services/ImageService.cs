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
                    caminhosImagens.Add(null); // Ou outra lógica se necessário
                    continue;
                }

                // Lógica para salvar a imagem como antes
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "img", nomeAcomodacao.Replace(" ", "_"));
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imagem.FileName);
                string filePath = Path.Combine(uploadDir, fileName);

                if (File.Exists(filePath))
                {
                    caminhosImagens.Add(null); // Ou outra lógica se necessário
                    continue;
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imagem.CopyToAsync(fileStream);
                }

                // Adicione o caminho relativo à lista de caminhos, substituindo barras invertidas por barras normais
                caminhosImagens.Add(Path.Combine("img", nomeAcomodacao.Replace(" ", "_"), fileName).Replace("\\", "//"));
            }

            return caminhosImagens;
        }


    }
}