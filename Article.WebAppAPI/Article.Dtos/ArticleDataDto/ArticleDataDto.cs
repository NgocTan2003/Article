using Article.Common.CommonBase;
using Article.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dtos.Datas
{
    public class ArticleDataDto : DateCommon, SeoCommon
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Decripstion { get; set; }
        public string PathImage { get; set; }
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public bool IsDelete { get; set; }
        public Status Status { get; set; }
        public Guid ArticleSubCategoryId { get; set; }
        public List<ArticleTag> ArticleTags { get; set; }


        public string? CreateBy { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? SeoKeyword { get; set; }
        public string? SeoDecripstion { get; set; }
        public string? SeoTitle { get; set; }
    }
}
