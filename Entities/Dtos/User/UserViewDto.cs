using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.User
{
    public class UserViewDto
    {
        public string Id { get; set; } = "";

        public string UserName { get; set; } = "";

        public bool IsAdmin { get; set; }
    }
}
