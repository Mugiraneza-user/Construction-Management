using mks.DTOs;

namespace mks.Interfaces
{
    public interface IOtpService
    {
        Task<String> GenerateOtpAsync( string email);

        Task SaveOtpAsync(string email, string otp);

        Task <ServiceResponse>ResendOtpAsync(string email );

        Task<bool> VerifyOtpAsync(string email, string otp);

        Task DeleteOtpAsync(string email);
      
    }
}