using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dtos.ArticlePermissionDto
{
    public class UpdateArticlePermission
    {
        [Required]
        public string RoleName { get; set; }
        [Required]
        public Guid FunctionId { get; set; }
        [Required]
        public bool CanCreate { set; get; }
        [Required]
        public bool CanRead { set; get; }
        [Required]
        public bool CanUpdate { set; get; }
        [Required]
        public bool CanDelete { set; get; }
    }
}
