using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dtos.UserDto
{
    public class AuthenticationOTPRequest
    {
        public string code { get; set; }
        public string userName { get; set; }
    }
}
