﻿using Alchemy.Domain;
using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;

namespace Alchemy.Application.Services
{
    public class UserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task Register(string username, string email, string password)
        {
            var hashedPassword = _passwordHasher.GenerateHash(password);

            var (user, error) = User.Create(Guid.NewGuid(), username, hashedPassword, email, new List<Appointment>());
            if (error != null)
            {
                throw new Exception(error);
            }

            await _userRepository.AddUser(user);
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if(!result)
                throw new Exception("Failed to login");

            return _jwtProvider.GenerateToken(user);
        }
    }
} 
