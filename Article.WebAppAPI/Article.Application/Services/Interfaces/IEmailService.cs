using Article.Common.ReponseBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.Services.Interfaces
{
    public interface IEmailService
    {
        Task<ResponseMessage> SendEmail(MessageEmail request);
        MessageEmail ChangeToMessageEmail(string To, string Subject, string Body);
    }
}
