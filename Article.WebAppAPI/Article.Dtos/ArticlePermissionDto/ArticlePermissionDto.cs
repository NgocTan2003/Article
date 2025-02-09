using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dtos.ArticlePermissionDto
{
    public class ArticlePermissionDto
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public int FunctionId { get; set; }
        public bool CanCreate { set; get; }
        public bool CanRead { set; get; }
        public bool CanUpdate { set; get; }
        public bool CanDelete { set; get; }
    }
}
