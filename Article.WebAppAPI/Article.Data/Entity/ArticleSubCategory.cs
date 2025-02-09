using Article.Common.CommonBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Article.Data.Entity
{
    public class ArticleSubCategory : DateCommon, SeoCommon
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? PathImage { get; set; }
        [Required]
        public bool IsDelete { get; set; }
        [JsonIgnore]
        public ArticleCategory ArticleCategory { get; set; }
        [ForeignKey("ArticleCategoryId")]
        public Guid ArticleCategoryId { get; set; }
        public int DisplayOrder { get; set; }
        public List<ArticleData> ArticleDatas { get; set; }


        public string? CreateBy { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? SeoKeyword { get; set; }
        public string? SeoDecripstion { get; set; }
        public string? SeoTitle { get; set; }
    }
}
