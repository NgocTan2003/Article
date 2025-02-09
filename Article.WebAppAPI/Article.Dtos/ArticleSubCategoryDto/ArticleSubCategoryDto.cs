using Article.Common.CommonBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dtos.SubCategories
{
    public class ArticleSubCategoryDto : DateCommon, SeoCommon
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? PathImage { get; set; }
        public bool IsDelete { get; set; }
        public Guid ArticleCategoryId { get; set; }
        public int DisplayOrder { get; set; }
        //public List<ArticleData> ArticleDatas { get; set; }


        public string? CreateBy { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? SeoKeyword { get; set; }
        public string? SeoDecripstion { get; set; }
        public string? SeoTitle { get; set; }
    }
}
