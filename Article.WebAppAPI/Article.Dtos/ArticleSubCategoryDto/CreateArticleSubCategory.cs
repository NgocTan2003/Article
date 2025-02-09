using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace Article.Dtos.ArticleSubCategoryDto
{
    public class CreateArticleSubCategory
    {
        [Required(ErrorMessage = "Bạn phải nhập tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập mô tả")]
        public string Description { get; set; }
        public IFormFile? UploadFile { get; set; }
        [Required(ErrorMessage = "Bạn phải chọn trạng thái")]
        public bool IsDelete { get; set; }
        [Required(ErrorMessage = "Bạn phải chọn danh mục")]
        public Guid ArticleCategoryId { get; set; }
        [Required(ErrorMessage = "Bạn phải chọn vị trí")]
        public int DisplayOrder { get; set; }


        public string? CreateBy { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? SeoKeyword { get; set; }
        public string? SeoDecripstion { get; set; }
        public string? SeoTitle { get; set; }
    }
}
