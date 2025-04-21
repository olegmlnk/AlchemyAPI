using Alchemy.Domain.Models;
using Alchemy.Infrastructure.Entities;
using AutoMapper;

namespace Alchemy.Infrastructure.Mappings
{
    public class UserMapper : Profile
    {
        public UserMapper() 
        {
            CreateMap<UserEntity, User>().ReverseMap();
        }
    }
}
