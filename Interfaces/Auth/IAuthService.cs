using mks.Dtos;
using mks.DTOs;

namespace mks.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);

        Task<AuthResponseDto> LoginAsync(LoginDto dto);

        Task<AuthResponseDto> VerifyOtpAsync(VerifyOtpDto dto);

        Task<AuthResponseDto> ForgotPasswordAsync(ForgotPasswordDto dto);

        Task<AuthResponseDto> ResetPasswordAsync(ResetPasswordDto dto);



    }
}