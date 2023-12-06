using Application.Interfaces;
using Application.ViewModel;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<int> CreateManyAsync(IEnumerable<ImagensViewModel> uploadImagem)
        {
            try
            {
                return await _IImagensApp.CreateManyAsync(uploadImagem);
            }
            catch (Exception)
            {
                return 0;
            }

        }

        public async Task<List<ImagensViewModel>> FindImagesById(int id)
        {
            try
            {
                return await _IImagensApp.FindOneAsyncFindImageFromAcomodationID(id);
            }
            catch (Exception)
            {
                return new();
            }
        }

        private static bool IsValidImage(IFormFile imagem)
        {
            return imagem != null && imagem.Length > 0;
        }

        private string GetUploadDirectory(string nomeAcomodacao)
        {
            string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "img", nomeAcomodacao.Replace(" ", "_"));

            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            return uploadDir;
        }

        private static string GetUniqueFileName(IFormFile imagem, string uploadDir)
        {
            string fileName = Path.GetFileName(imagem.FileName);
            string filePath = Path.Combine(uploadDir, fileName);

            int count = 1;
            while (File.Exists(filePath))
            {
                fileName = $"{Path.GetFileNameWithoutExtension(imagem.FileName)}_{count}{Path.GetExtension(imagem.FileName)}";
                filePath = Path.Combine(uploadDir, fileName);
                count++;
            }

            return fileName;
        }

        private static string GetRelativeImagePath(string nomeAcomodacao, string fileName)
        {
            return Path.Combine("img", nomeAcomodacao.Replace(" ", "_"), fileName).Replace("\\", "//");
        }

        public async Task<List<string>> AddImagesAsync(List<IFormFile> imagens, string nomeAcomodacao)
        {
            List<string> caminhosImagens = new();

            try
            {
                foreach (IFormFile imagem in imagens)
                {
                    if (IsValidImage(imagem))
                    {
                        string uploadDir = GetUploadDirectory(nomeAcomodacao);
                        string fileName = GetUniqueFileName(imagem, uploadDir);
                        string filePath = Path.Combine(uploadDir, fileName);

                        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))

                        {
                            await imagem.CopyToAsync(fileStream);
                        }

                        caminhosImagens.Add(GetRelativeImagePath(nomeAcomodacao, fileName));
                    }
                }
            }
            catch (Exception ex)
            {
                // Trate a exceção de forma apropriada, como logar ou retornar informações ao cliente
                Console.WriteLine($"Erro ao adicionar imagens: {ex.Message}");
                caminhosImagens = null;
            }

            return caminhosImagens;
        }

        public async Task<bool> RemoveImagesByIDFromDB(List<int> imagensParaExcluir)
        {
            try
            {
                List<int> sucess = new();
                if (imagensParaExcluir != null && imagensParaExcluir.Any())
                {

                    foreach (int imagemId in imagensParaExcluir)
                    {

                        ImagensViewModel imagemParaExcluir = await _IImagensApp.FindOneAsync(imagemId);
                        if (imagemParaExcluir != null)
                        {


                            Imagens imagemExcluir = new()
                            {
                                Id = imagemParaExcluir.Id,
                                Nome = imagemParaExcluir.Nome,
                                Id_Acomodacao = imagemParaExcluir.Id_Acomodacao,
                                RotaImagem = imagemParaExcluir.RotaImagem,
                            };

                            sucess.Add(await _IImagensApp.Remove(imagemExcluir));
                        }
                        if (File.Exists(Path.Combine(_webHostEnvironment.WebRootPath, imagemParaExcluir.RotaImagem)))
                        {
                            File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, imagemParaExcluir.RotaImagem));
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao remover diretório: {ex.Message}");
                return false;
            }
        }

        public bool RemoveDirectory(List<ImagensViewModel> listaExclusao)
        {
            try
            {
                if (listaExclusao.Any())
                {
                    string diretorioPai = Path.GetDirectoryName(Path.Combine(_webHostEnvironment.WebRootPath, listaExclusao.First().Nome.Replace(" ", "_")));

                    foreach (string arquivo in Directory.EnumerateFiles(diretorioPai))
                    { File.Delete(arquivo); }

                    if (!Directory.EnumerateFiles(diretorioPai).Any()) Directory.Delete(diretorioPai);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao remover diretório: {ex.Message}");
                return false;
            }
        }
    }
}