using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Common.ReponseBase
{
    public class ResponseListItem<T> where T : class
    {
        public List<T> Items { get; set; }
        public string? Message { get; set; }
        public int? StatusCode { get; set; }
    }
}
