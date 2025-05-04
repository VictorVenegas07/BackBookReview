using System.Net;
using System.Net.Mail;
using System.Reflection;
using EmailSend.Models;
using EmailSend.Templates;
using RazorLight;

namespace EmailSend.Service;

public class EmailService
{
    private readonly RazorLightEngine _razorEngine;
    private readonly string _templatePath;

    public EmailService()
    {
        _templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates");

        _razorEngine = new RazorLightEngineBuilder()
    .UseFileSystemProject(_templatePath)
    .UseMemoryCachingProvider()
    .Build();

    }

    public async Task SendWelcomeEmailAsync(string toEmail, EmailModel model)
    {
        var smtpServer = "smtp.gmail.com";
        var smtpPort = 587;
        var enableSsl = true;
        var senderEmail = "pruebitatestnico@gmail.com";
        var userName = "pruebitatestnico@gmail.com";
        var password = "rlvk nfwp yrwx cnqo";

        string body = await _razorEngine.CompileRenderAsync("WelcomeEmail.cshtml", model);

        using (var client = new SmtpClient(smtpServer, smtpPort))
        {
            client.EnableSsl = enableSsl;
            client.Credentials = new NetworkCredential(userName, password);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = "Aviso de validación de solicitud de exportación de bienes",
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);
            await client.SendMailAsync(mailMessage);
        }


    }
}

//jrkq ablc xlzq meso