using Article.Common.CommonBase;
using Article.Data.Entity;
using Article.Dtos.SubCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dtos.Categories
{
    public class ArticleCategoryDto : DateCommon, SeoCommon
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PathImage { get; set; }
        public bool IsDelete { get; set; }
        public int DisplayOrder { get; set; }
        public List<ArticleSubCategory> ArticleSubCategories { get; set; }


        public string? CreateBy { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? SeoKeyword { get; set; }
        public string? SeoDecripstion { get; set; }
        public string? SeoTitle { get; set; }
    }
}
