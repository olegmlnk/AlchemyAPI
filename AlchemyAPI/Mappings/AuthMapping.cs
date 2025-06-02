using Alchemy.Domain.Models;
using AlchemyAPI.Contracts;
using AutoMapper;

namespace AlchemyAPI.Mappings
{
    public class AuthMapping : Profile
    {
        public AuthMapping()
        {
            CreateMap<RegisterUserRequest, User>();
        }
    }
}