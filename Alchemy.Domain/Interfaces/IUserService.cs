using System.Security.Claims;
using Alchemy.Domain.Contracts;
using Microsoft.AspNetCore.Identity.Data;

namespace Alchemy.Domain.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterUserRequest request);
        Task LoginAsync(LoginUserRequest request);
        Task RefreshTokenAsync(string? refreshToken);
        Task LoginWithGoogleAsync(ClaimsPrincipal? claimsPrincipal);
    }
}