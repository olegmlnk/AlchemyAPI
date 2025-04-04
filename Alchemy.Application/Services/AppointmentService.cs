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

        public async Task<List<Appointment>> GetAppointment()
        {
            return await _appointmentRepository.GetAppointment();
        }

        public async Task<long> GetAppointmentById(long id)
        {
            return await _appointmentRepository.GetAppointmentById(id);
        }

        public async Task<long> CreateAppointment(Appointment appointment)
        {
            return await _appointmentRepository.CreateAppointment(appointment);
        }

        public async Task<long> UpdateAppointment(long id, DateTime appointmentDate, string description, long masterId, long serviceId, long userId)
        {
            return await _appointmentRepository.UpdateAppointment(id, appointmentDate, description, masterId, serviceId, userId);
        }
        public async Task<long> DeleteAppointment(long id)
        {
            return await _appointmentRepository.DeleteAppointment(id);
        }

        public async Task<List<MasterSchedule>> GetAvailableSlots(long masterId)
        {
            return await _masterScheduleRepository.GetAvailableSlots(masterId);
        }

        public async Task<bool> BookAppointment(long slotId, long clientId)
        {
            return await _masterScheduleRepository.BookSlot(slotId);
        }
    }
}
