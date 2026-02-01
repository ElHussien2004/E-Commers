using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObject.IdentityDTOS
{
    public class RegisterDTO
    {

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        public string? UserName { get; set; } = "ahmedmohamed";

        public string DisplayName { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }   
    }
}
