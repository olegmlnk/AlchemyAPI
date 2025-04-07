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
            return await _appointmentRepository.GetAllAppointmentsAsync();
        }

        public async Task<long> GetAppointmentById(long id)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(id);
        }

        public async Task<long> CreateAppointment(Appointment appointment)
        {
            return await _appointmentRepository.CreateAppointmentAsync(appointment);
        }

        public async Task<long> UpdateAppointment(long id, DateTime appointmentDate, string description, long masterId, long serviceId, long userId)
        {
            return await _appointmentRepository.UpdateAppointment(id, appointmentDate, description, masterId, serviceId, userId);
        }
       
        public async Task<List<Appointment>> GetAppointmentsByUserIdAsync(long userId)
        {

        }

        public async Task<List<Appointment>> GetAppointmentsByMasterIdAsync(long masterId)
        {


        }

        public async Task<bool> CancelAppointmentAsync(long appointmentId)
        {

        }
    }
}
