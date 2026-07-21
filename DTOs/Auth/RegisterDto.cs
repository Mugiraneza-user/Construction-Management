using System.ComponentModel.DataAnnotations;


namespace mks.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(150)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(254)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required]
       

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Telephone {get; set;} = string.Empty;
    }
}