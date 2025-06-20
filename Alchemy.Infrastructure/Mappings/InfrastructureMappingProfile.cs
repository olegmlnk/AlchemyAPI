using Alchemy.Domain.Models;
using Alchemy.Infrastructure.Entities;
using AutoMapper;

namespace Alchemy.Infrastructure.Mappings;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<Appointment, AppointmentEntity>().ReverseMap();

        CreateMap<Master, MasterEntity>()
            .ForMember(dest => dest.Appointments, 
                opt => opt.MapFrom(src => src.Appointments))
            .ForMember(dest => dest.ScheduleSlots, 
                opt => opt.MapFrom(src => src.MasterSchedules))
            .ReverseMap();

        CreateMap<Service, ServiceEntity>()
            .ForMember(dest => dest.Price, 
                opt => opt.MapFrom(src => (decimal)src.Price))
            .ReverseMap();

        CreateMap<MasterSchedule, MasterScheduleEntity>().ReverseMap();
    }
}