using System.Security.Claims;
using Alchemy.Domain.Contracts;
using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using Alchemy.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace Alchemy.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthTokenProcessor _authTokenProcessor;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<User> userManager, IAuthTokenProcessor authTokenProcessor, IUserRepository userRepository)
        {
            _userManager = userManager;
            _authTokenProcessor = authTokenProcessor;
            _userRepository = userRepository;
        }

        public async Task RegisterAsync(RegisterUserRequest request)
        {
            var userExists = await _userManager.FindByEmailAsync(request.Email) != null;

            if (userExists)
                throw new UserAlreadyExistsException(request.Email);
        
            var (user, error) = User.Create(
                request.Email,
                request.FirstName, 
                request.LastName    
            );
        
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
                throw new RegistrationFailedException(result.Errors.Select(e => e.Description));
        }
        
        public async Task LoginAsync(LoginUserRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new LoginFailedException(request.Email);
        
            var (jwtToken, expirationTimeInUtc) = _authTokenProcessor.GenerateJwtToken(user);
            var refreshToken = _authTokenProcessor.GenerateRefreshToken();
        
            var refreshTokenExpirationTokenTimeAtUtc = DateTime.UtcNow.AddDays(7);
        
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiresAtUtc = refreshTokenExpirationTokenTimeAtUtc;

            await _userManager.UpdateAsync(user);
        
            _authTokenProcessor.WriteAuthTokenToHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationTimeInUtc);
        
            //TODO hide the refresh token from the client side, don't hardcode it 
            _authTokenProcessor.WriteAuthTokenToHttpOnlyCookie("REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationTokenTimeAtUtc);
        }
        
        public async Task RefreshTokenAsync(string? refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new RefreshTokenException("Refresh token is missing.");
        }

        var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken);

        if (user == null)
        {
            throw new RefreshTokenException("Unable to retrieve user for refresh token");
        }

        if (user.RefreshTokenExpiresAtUtc < DateTime.UtcNow)
        {
            throw new RefreshTokenException("Refresh token is expired.");
        }
        
        var (jwtToken, expirationDateInUtc) = _authTokenProcessor.GenerateJwtToken(user);
        var refreshTokenValue = _authTokenProcessor.GenerateRefreshToken();

        var refreshTokenExpirationDateInUtc = DateTime.UtcNow.AddDays(7);

        user.RefreshToken = refreshTokenValue;
        user.RefreshTokenExpiresAtUtc = refreshTokenExpirationDateInUtc;

        await _userManager.UpdateAsync(user);
        
        _authTokenProcessor.WriteAuthTokenToHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationDateInUtc);
        _authTokenProcessor.WriteAuthTokenToHttpOnlyCookie("REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationDateInUtc);
    }

    public async Task LoginWithGoogleAsync(ClaimsPrincipal? claimsPrincipal)
    {
        if(claimsPrincipal == null)
            throw new ExternalLoginProviderException("Google", "ClaimsPrincipal is null.");
        
        var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        
        if(email == null)
            throw new ExternalLoginProviderException("Google", "Email claim is missing.");
        
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            var newUser = new User
            {
                UserName = email,
                Email = email,
                FirstName = claimsPrincipal.FindFirstValue(ClaimTypes.GivenName) ?? "Unknown",
                LastName = claimsPrincipal.FindFirstValue(ClaimTypes.Surname) ?? "Unknown",
                EmailConfirmed = true
            };
            
            var result = await _userManager.CreateAsync(newUser);
            
            if(!result.Succeeded)
                throw new ExternalLoginProviderException("Google", 
                    $"Unable to create user: {string.Join(", ", result.Errors.
                        Select(e => e.Description))}");
            
            user = newUser;
        }

        var info = new UserLoginInfo("Google",
            claimsPrincipal.FindFirstValue(ClaimTypes.Email) ?? string.Empty, "Google");
        
        var loginResult = await _userManager.AddLoginAsync(user, info);
        
        if (!loginResult.Succeeded)
            throw new ExternalLoginProviderException("Google", 
                $"Unable to link Google account: {string.Join(", ", loginResult.Errors.
                    Select(e => e.Description))}");
        
        var (jwtToken, expirationDateInUtc) = _authTokenProcessor.GenerateJwtToken(user);
        var refreshTokenValue = _authTokenProcessor.GenerateRefreshToken();

        var refreshTokenExpirationDateInUtc = DateTime.UtcNow.AddDays(7);

        user.RefreshToken = refreshTokenValue;
        user.RefreshTokenExpiresAtUtc = refreshTokenExpirationDateInUtc;

        await _userManager.UpdateAsync(user);
        
        _authTokenProcessor.WriteAuthTokenToHttpOnlyCookie("ACCESS_TOKEN", jwtToken, expirationDateInUtc);
        _authTokenProcessor.WriteAuthTokenToHttpOnlyCookie("REFRESH_TOKEN", user.RefreshToken, refreshTokenExpirationDateInUtc);
    }
    }
} 
