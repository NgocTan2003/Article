using Article.Common.CommonBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dtos.Users
{
    public class ArticleUserDto : DateCommon, SeoCommon
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string Avatar { get; set; }
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
