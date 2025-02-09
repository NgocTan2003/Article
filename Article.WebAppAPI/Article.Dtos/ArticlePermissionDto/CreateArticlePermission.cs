using Article.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dtos.ArticlePermissionDto
{
    public class CreateArticlePermission
    {
        public string RoleName { get; set; }
        public List<ArticlePermission> ListPermission { get; set; }
    }
}
