using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class RegisterModel
    {
        [Required]
        public required string username { get; set; }

        [Required]
        public required string password { get; set; }

        [Required]
        public required string email { get; set; }


    }
}
