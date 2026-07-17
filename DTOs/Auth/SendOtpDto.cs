using  System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;
using Microsoft.Net.Http.Headers;



namespace mks.Dtos
{
    public class SendOtpDto
    {
        public string Email {get ; set ;} = string.Empty;
    };
}
