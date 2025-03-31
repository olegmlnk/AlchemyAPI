using Alchemy.Application.Services;
using AlchemyAPI.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AlchemyAPI.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);
            app.MapPost("login", Login);

            return app;
        }

        private static async Task<IResult> Register(
            RegisterUserRequest registerUserRequest, 
            UserService userService)
        {
            await userService.Register(registerUserRequest.Username, registerUserRequest.Email,
                registerUserRequest.Password);

            return Results.Ok();
        }

        private static async Task<IResult> Login(LoginUserRequest loginUserRequest, UserService userService)
        {
            var token = await userService.Login(loginUserRequest.Email, loginUserRequest.Password);

            return Results.Ok();
        }
    }
}
