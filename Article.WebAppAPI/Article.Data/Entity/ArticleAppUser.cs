using Article.Common.CommonBase;
using Microsoft.AspNetCore.Identity;

namespace Article.Data.Entity
{
    public class ArticleAppUser : IdentityUser, DateCommon, SeoCommon
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Adress { get; set; }
        public string? Avatar { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? TokenExpirationTime { get; set; }
        public DateTime? RefreshTokenExpirationTime { get; set; }

        public string? CreateBy { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }
        public string? SeoKeyword { get; set; }
        public string? SeoDecripstion { get; set; }
        public string? SeoTitle { get; set; }
    }
}
