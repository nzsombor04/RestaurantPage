using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.User
{
    public class UserInputDto
    {
        [MinLength(1)]
        public required string Email { get; set; } = "";

        [MinLength(5)]
        public required string Username { get; set; } = "";

        [MinLength(5)]
        public required string Password { get; set; } = "";
    }
}
