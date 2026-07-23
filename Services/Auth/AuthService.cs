using mks.Data;
using mks.DTOs;
using mks.Helpers;
using mks.Models;
using mks.Interfaces;
using Microsoft.EntityFrameworkCore;
using mks.Dtos;
using System.Diagnostics;

namespace mks.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IOtpService _otpService;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;

        public AuthService(
            ApplicationDbContext context,
            IOtpService otpService,
            IJwtService jwtService,
            IEmailService emailService)
        {
            _context = context;
            _otpService = otpService;
            _jwtService = jwtService;
            _emailService = emailService;
        }


        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var response = new AuthResponseDto();

             var connection = _context.Database.GetDbConnection();

    
            var usernameExists = await _context.Users
                .AnyAsync(x => x.Username == dto.Username);

            if (usernameExists)
            {
                response.Success = false;
                response.Message = "Username already exists.";

                return response;
            }

            var emailExists = await _context.Users
                .AnyAsync(x => x.Email == dto.Email);

            if (emailExists)
            {
                response.Success = false;
                response.Message = "Email already exists.";

                return response;
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = PasswordHasher.Hash(dto.Password),
                Telephone= dto.Telephone,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                IsActive = true,
                Disabled = false,
                IsStaff = false,
                IsSuperuser = false,

                EmailVerified = false,

                DateJoined = DateTime.UtcNow
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            
            var otp = await _otpService.GenerateOtpAsync(dto.Email);

            await _otpService.SaveOtpAsync(
                user.Email,
                otp);

            response.Success = true;
            response.Message = "Registration successful. OTP has been sent to your email.";

            

            return response;
        }
        

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var response = new AuthResponseDto();

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
            {
                response.Success = false;
                response.Message = "Invalid email or password.";

                return response;
            }

            if (!user.IsActive)
            {
                response.Success = false;
                response.Message = "Your account is inactive.";

                return response;
            }

            if (user.Disabled)
            {
                response.Success = false;
                response.Message = "Your account has been disabled.";

                return response;
            }

            if (!user.EmailVerified)
            {
                response.Success = false;
                response.Message = "Please verify your email before logging in.";

                return response;
            }

            var validPassword = PasswordHasher.Verify(
                dto.Password,
                user.Password);

            if (!validPassword)
            {
                response.Success = false;
                response.Message = "Invalid email or password.";

                return response;
            }

            user.LastLogin = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var token = _jwtService.GenerateToken(user);

            response.Success = true;
            response.Message = "Login successful.";
            response.Token = token;
            
            

            return response;
        }
       

        public async Task<AuthResponseDto> VerifyOtpAsync(
            VerifyOtpDto dto)
        {
            var response = new AuthResponseDto();

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";

                return response;
            }

            var verified = await _otpService.VerifyOtpAsync(
                dto.Email,
                dto.Code);

            if (!verified)
            {
                response.Success = false;
                response.Message = "Invalid or expired OTP.";

                return response;
            }

            user.EmailVerified = true;
             Console.WriteLine(user.EmailVerified);

                Console.WriteLine(_context.Entry(user).State);

                var rows = await _context.SaveChangesAsync();

                Console.WriteLine(rows);
            
            //  await _context.SaveChangesAsync();
            

            await _otpService.DeleteOtpAsync(dto.Email);

            response.Success = true;
            response.Message = "Email verified successfully.";
         

            return response;
        }
        
        
               

                public async Task<AuthResponseDto> ForgotPasswordAsync(
                    ForgotPasswordDto dto)
                {
                    var response = new AuthResponseDto();

                    var user = await _context.Users
                        .FirstOrDefaultAsync(x => x.Email == dto.Email);

                    if (user == null)
                    {
                        response.Success = false;
                        response.Message = "No account was found with that email.";

                        return response;
                    }

                    var otp = await _otpService.GenerateOtpAsync(dto.Email);

                    await _otpService.SaveOtpAsync(
                        user.Email,
                        otp);

                    response.Success = true;
                    response.Message =
                        "A password reset OTP has been sent to your email.";

                    return response;
                }

               

                public async Task<AuthResponseDto> ResetPasswordAsync(
                    ResetPasswordDto dto)
                {
                    var response = new AuthResponseDto();

                    var user = await _context.Users
                        .FirstOrDefaultAsync(x => x.Email == dto.Email);

                    if (user == null)
                    {
                        response.Success = false;
                        response.Message = "User not found.";

                        return response;
                    }

                    var verified = await _otpService.VerifyOtpAsync(
                        dto.Email,
                        dto.Code);

                    if (!verified)
                    {
                        response.Success = false;
                        response.Message = "Invalid or expired OTP.";

                        return response;
                    }
                    
                    if (PasswordHasher.Verify(dto.NewPassword, user.Password))
                    {
                        response.Success= false;
                        response.Message = "Password has been used before, create a new one";
                    }
                    user.Password = PasswordHasher.Hash(dto.NewPassword);

                    await _context.SaveChangesAsync();

                    await _otpService.DeleteOtpAsync(dto.Email);

                    response.Success = true;
                    response.Message =
                        "Password has been reset successfully.";

                    return response;
                }
                

          

       
    }
}