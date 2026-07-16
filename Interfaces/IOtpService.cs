namespace mks.Interfaces
{
    public interface IOtpService
    {
        Task<string> GenerateOtpAsync();

        Task SaveOtpAsync(string email, string otp);

        Task<bool> VerifyOtpAsync(string email, string otp);

        Task DeleteOtpAsync(string email);
    }
}