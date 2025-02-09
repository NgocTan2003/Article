using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Common.ReponseBase
{
    public class S3Response
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = "";
        public string? PresignedUrl { get; set; }
    }
}
