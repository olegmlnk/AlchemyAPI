using Alchemy.Domain.Models;
using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Repositories;

namespace Alchemy.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<bool> CancelAppointmentAsync(long appointmentId)
        {
            return await _appointmentRepository.CancelAppointmentAsync(appointmentId);
        }

        public async Task<long?> CreateAppointmentAsync(Appointment appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment));

            var id = await _appointmentRepository.CreateAppointmentAsync(appointment);

            if (id == null)
                throw new Exception("Appointment creation failed");

            return id;
        }

        public async Task<List<Appointment?>> GetAllAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAppointmentsAsync();
        }

        public async Task<Appointment?> GetAppointmentByIdAsync(long id)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(id);
        }

        public async Task<List<Appointment>> GetAppointmentByMasterIdAsync(long masterId)
        {
            return await _appointmentRepository.GetAppointmentByMasterIdAsync(masterId);
        }

        public async Task<List<Appointment>> GetAppointmentByUserIdAsync(string userId)
        {
            return await _appointmentRepository.GetAppointmentByUserIdAsync(userId);
        }

        public async Task<long?> UpdateAppointmentAsync(long id, string scheduleSlotId, string description, string userId, long masterId, long serviceId)
        {
            return await _appointmentRepository.UpdateAppointmentAsync(id, scheduleSlotId, description, userId, masterId, serviceId);
        }
    }
}
