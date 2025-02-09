using Article.Common.CommonBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Data.Entity
{
    public class ArticleData : DateCommon, SeoCommon
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Decripstion { get; set; }
        public string PathImage { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public bool IsDelete { get; set; }
        public Status Status { get; set; }
        public ArticleSubCategory ArticleSubCategory { get; set; }
        [ForeignKey("ArticleSubCategoryId")]
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
