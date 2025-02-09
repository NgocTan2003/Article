using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dtos.ArticleCategoryDto
{
    public class UpdateArticleCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? UploadFile { get; set; }
        public bool IsDelete { get; set; }
        public int DisplayOrder { get; set; }

        public string? CreateBy { get; set; }
        public string? LastUpdateBy { get; set; }
        public string? LastDeleteBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string? SeoKeyword { get; set; }
        public string? SeoDecripstion { get; set; }
        public string? SeoTitle { get; set; }
    }
}
