
namespace mks.Helpers
{
    public class EmailService : IEmailService
    {
        public async Task SendOtpAsync(string email, string otp)
        {
            // TODO:
            // Replace this with real email sending.

            Console.WriteLine($"OTP for {email}: {otp}");

            await Task.CompletedTask;
        }
    }
}