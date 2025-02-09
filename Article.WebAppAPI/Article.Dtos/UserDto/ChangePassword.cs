using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dtos.UserDto
{
    public class ChangePassword
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PasswordOld { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must have at least 6 characters.")]
        public string PasswordNew { get; set; }
        [Compare("PasswordNew", ErrorMessage = "The PasswordNew and confirm PasswordNew do not match.")]
        public string ConfirmPasswordNew { get; set; }
    }
}
