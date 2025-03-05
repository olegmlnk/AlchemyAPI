using Alchemy.Domain.Models;
using Alchemy.Domain.Interfaces;

namespace Alchemy.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<List<Appointment>> GetAppointment()
        {
            return await _appointmentRepository.GetAppointment();
        }

        public async Task<Guid> GetAppointmentById(Guid id)
        {
            return await _appointmentRepository.GetAppointmentById(id);
        }

        public async Task<Guid> CreateAppointment(Appointment appointment)
        {
            return await _appointmentRepository.CreateAppointment(appointment);
        }

        public async Task<Guid> UpdateAppointment(Guid id, DateTime appointmentDate, string description)
        {
            return await _appointmentRepository.UpdateAppointment(id, appointmentDate, description);
        }
        public async Task<Guid> DeleteAppointment(Guid id)
        {
            return await _appointmentRepository.DeleteAppointment(id);
        }
    }
}
