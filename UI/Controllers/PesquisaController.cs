using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace UI.Controllers
{
    [Route("Home")]
    public class BookingController : Controller
    {

        [HttpPost("SendBookingInfo")]
        public async Task<IActionResult> SendBookingInfo(BookingViewModel model)
        {
            try
            {
                // Configurar as informações do servidor de e-mail do Gmail
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("email", "senha"),
                    EnableSsl = true,
                };

                // Construir o corpo do e-mail com base nas informações fornecidas
                string body = $"Solicitação de Orçamento:\n\n" +
                           $"Data de Entrada: {model.Entrada}\n" +
                           $"Data de Saída: {model.Saida}\n" +
                           $"Pets: {(model.Pet == "Sim" ? "Sim" : "Não")}\n" +
                           $"Acomodação: {model.Acomodacao}";

                // Configurar a mensagem de e-mail
                MailMessage message = new MailMessage("remetente", "destinatario")
                {
                    Subject = "Pedido de Orçamento",
                    Body = body,
                    IsBodyHtml = false
                };

                // Enviar e-mail assíncrono
                await smtpClient.SendMailAsync(message);

                return RedirectToAction("Index", "Home"); // Redirecionar para a página inicial após o envio bem-sucedido
            }
            catch (Exception)
            {
                // Trate exceções aqui, você pode querer logá-las ou fornecer uma mensagem de erro ao usuário
                return RedirectToAction("Error", "Home");
            }
        }
    }
}