using Article.Application.Services.Interfaces;
using Article.Common.ReponseBase;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Article.Application.Services.Implements
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public MessageEmail ChangeToMessageEmail(string To, string Subject, string Body)
        {
            var message = new MessageEmail();
            message.To = To;
            message.Subject = Subject;
            message.Body = Body;
            return message;
        }

        public async Task<ResponseMessage> SendEmail(MessageEmail request)
        {
            var response = new ResponseMessage();
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailConfiguration")["EmailUsername"]));
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                await smtp.ConnectAsync(_config.GetSection("EmailConfiguration")["EmailHost"], 465, true);

                await smtp.AuthenticateAsync(_config.GetSection("EmailConfiguration")["EmailUsername"], _config.GetSection("EmailConfiguration")["EmailPassword"]);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                response.StatusCode = StatusCodes.Status200OK;
                response.Message = "Send Email Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Send Email: " + ex.Message;
            }
            return response;
        }
    }
}
