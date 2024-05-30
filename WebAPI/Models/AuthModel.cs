using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class AuthModel
    {
        [Required]
        public required string username { get; set; }

        [Required]
        public required string password { get; set; }

        public string? redirection { get; set; }
    }
}
