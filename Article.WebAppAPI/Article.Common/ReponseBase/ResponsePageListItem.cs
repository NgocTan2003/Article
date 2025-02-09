using Article.Common.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Common.ReponseBase
{
    public class ResponsePageListItem<T> where T : class
    {
        public PagedList<T> Items { get; set; }
        public string? Message { get; set; }
        public int? StatusCode { get; set; }
    }
}
