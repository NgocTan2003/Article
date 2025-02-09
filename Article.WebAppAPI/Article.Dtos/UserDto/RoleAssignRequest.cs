using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Dtos.UserDto
{
    public class RoleAssignRequest
    {
        public bool Status { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
    }
}
