using System.ComponentModel.DataAnnotations;

namespace mks.DTOs
{
    public class ResendOtpDto
    {
        public string Email {get; set;} =string.Empty;
    }
}