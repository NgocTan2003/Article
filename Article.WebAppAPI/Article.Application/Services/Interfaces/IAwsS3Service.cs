using Article.Common.ReponseBase;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.Services.Interfaces
{
    public interface IAwsS3Service
    {
        Task<S3Response> UploadFile(IFormFile file, string? prefix, string namefile);
        Task<ResponseMessage> DeleteFileAsync(string key);
    }
}
