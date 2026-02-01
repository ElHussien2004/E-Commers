using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransfareObject.IdentityDTOS
{
    public class UserDTO
    {
        
        public string Email { get; set; }
        public string Token { get; set; }
        public string DisplayName { get; set; }
    }
}
