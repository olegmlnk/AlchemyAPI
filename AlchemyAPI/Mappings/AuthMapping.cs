using Alchemy.Domain.Contracts;
using Alchemy.Domain.Models;
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