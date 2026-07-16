using Microsoft.AspNetCore.Mvc;
using mks.Dtos;
using mks.DTOs;
using mks.Interfaces;
using mks.Services;

namespace mks.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IOtpService _otpService;

        public AuthController(IAuthService authService , IOtpService otpService)  
        {
            _authService = authService;
            _otpService = otpService;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var result = await _authService.ForgotPasswordAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

       

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var result = await _authService.ResetPasswordAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
          // Generate and send OTP
        [HttpPost("send")]
        public async Task<IActionResult> SendOtp([FromBody]SendOtpDto dto)
        {
            var result= await _otpService.GenerateOtpAsync(dto.Email);
            
            return Ok(new
            {
                message = "OTP sent successfully"
            });
        }
         [HttpPost("resend")]
        public async Task<IActionResult> ResendOtp([FromBody] SendOtpDto dto)
        {
            var result = await _otpService.ResendOtpAsync(dto.Email);
             if (!result.Success)
                return BadRequest(result);
            return Ok(new
            {
                message = "OTP resent successfully"
            });
        }
         [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpDto dto)
        {
            var result = await _authService.VerifyOtpAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

    }
}