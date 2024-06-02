using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HttpModels
{
    public class PasswordResetModel
    {
        public string Email { get; set; } = String.Empty;
        public string? PasswordReset { get; set; } = String.Empty;
    }
}
