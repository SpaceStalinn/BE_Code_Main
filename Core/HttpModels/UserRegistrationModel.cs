using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HttpModels
{
    public class UserRegistrationModel
    {
        public string Username {  get; set; } = string.Empty;   
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
