using System.ComponentModel.DataAnnotations;

namespace mks.DTOs
{
    public class VerifyOtpDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(6)]
        public string Code { get; set; } = string.Empty;
    }
}