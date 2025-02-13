﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dtos.UserDto
{
    public class ManagerRegister
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string Adress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile? UploadFile { get; set; }
        public IList<string> Roles { get; set; }
    }
}
