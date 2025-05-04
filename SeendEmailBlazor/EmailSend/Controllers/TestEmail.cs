using EmailSend.Models;
using EmailSend.Service;
using EmailSend.Templates;
using Microsoft.AspNetCore.Mvc;

namespace EmailSend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestEmail : ControllerBase
    {

        [HttpGet]
        [Route("SendEmail")]
        public async Task<object> Get([FromQuery] string email, string UserName)
        {
            var emailService = new EmailService();
            var estados = new[] { "Aprobado", "Pendiente", "Rechazado", "En revisión" };
            var titulos = new[] { "Bien Cultural", "Obra Histórica", "Documento Patrimonial", "Arte Religioso" };

            var random = new Random();
            var articles = new List<Article>();

            for (int i = 0; i < 5; i++) // Genera 5 artículos aleatorios
            {
                var numeroRadicado = random.Next(10000, 99999).ToString();
                var titulo = $"{titulos[random.Next(titulos.Length)]} {i + 1}";
                var estado = estados[random.Next(estados.Length)];

                articles.Add(new Article
                {
                    NumeroRadicado = numeroRadicado,
                    Titulo = titulo,
                    Estado = estado
                });
            }

            var model = new EmailModel
            {
                Username = UserName,
                Articles = articles,
                TotalArticles = 2
            };

            await emailService.SendWelcomeEmailAsync(email, model);

            var response = new
            {
                message = "Email sent successfully",
                email = email,
                articles = model.Articles,
                totalArticles = model.TotalArticles
            };

            return response;

        }
    }
}
