using Alchemy.Domain.Models;
using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Repositories;

namespace Alchemy.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMasterScheduleRepository _masterScheduleRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IMasterScheduleRepository masterScheduleRepository)
        {
            _appointmentRepository = appointmentRepository;
            _masterScheduleRepository = masterScheduleRepository;
        }

        public Task<bool> BookAppointment(long slotId, long clientId)
        {
            throw new NotImplementedException();
        }

        public Task<long> CreateAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Task<long> DeleteAppointment(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointment>> GetAppointment()
        {
            throw new NotImplementedException();
        }

        public Task<long> GetAppointmentById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<MasterSchedule>> GetAvailableSlots(long masterId)
        {
            throw new NotImplementedException();
        }

        public Task<long> UpdateAppointment(long id, DateTime appointmentDate, string description, long masterId, long serviceId, long userId)
        {
            throw new NotImplementedException();
        }
    }
}
