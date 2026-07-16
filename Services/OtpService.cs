
using Microsoft.EntityFrameworkCore;
using mks.Data;
using mks.Helpers;
using mks.Models;
using mks.Interfaces;



namespace mks.Services
{
    public class OtpService : IOtpService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public OtpService(
            ApplicationDbContext context,
            IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<string> GenerateOtpAsync()
        {
            var random = new Random();

            return random.Next(100000, 999999).ToString();
        }

        public async Task SaveOtpAsync(string email, string otp)
        {
            // Remove any previous OTP for this email
            var existingOtp = await _context.SignupOtps
                .FirstOrDefaultAsync(x => x.Email == email);

            if (existingOtp != null)
            {
                _context.SignupOtps.Remove(existingOtp);
                await _context.SaveChangesAsync();
            }

            var newOtp = new SignupOtp
            {
                Email = email,
                Code = otp,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                IsUsed = false
            };

            _context.SignupOtps.Add(newOtp);

            await _context.SaveChangesAsync();

            await _emailService.SendOtpAsync(email, otp);
        }

        public async Task<bool> VerifyOtpAsync(string email, string otp)
        {
            var record = await _context.SignupOtps
                .FirstOrDefaultAsync(x => x.Email == email);

            if (record == null)
                return false;

            if (record.IsUsed)
                return false;

            if (record.ExpiresAt < DateTime.UtcNow)
                return false;

            if (record.Code != otp)
                return false;

            record.IsUsed = true;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteOtpAsync(string email)
        {
            var otp = await _context.SignupOtps
                .FirstOrDefaultAsync(x => x.Email == email);

            if (otp != null)
            {
                _context.SignupOtps.Remove(otp);
                await _context.SaveChangesAsync();
            }
        }
    }
}