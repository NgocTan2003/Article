using System.ComponentModel.DataAnnotations;

namespace Article.Data.Entity
{
    public class ArticlePermission
    {
        [Key]
        public Guid Id { get; set; }
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
