using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Common.Seedwork
{
    public class PageRequest
    {
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 5;
        public string SearchText { get; set; } = "";

        public PageRequest() { }


        public PageRequest(int index, int? size, string searchText)
        {
            pageIndex = index;
            pageSize = size.HasValue && size > 0 ? size.Value : 5;
            SearchText = searchText;
        }
    }
}
