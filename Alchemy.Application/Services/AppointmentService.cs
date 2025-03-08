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

        public async Task<List<MasterSchedule>> GetAvailableSlot(Guid masterId)
        {
            return await _masterScheduleRepository.GetAvailableSlots(masterId);
        }

        public async Task<bool> BookAppointment(Guid slotId, Guid clientId)
        {
            var success = await _masterScheduleRepository.BookSlot(slotId);

            if(!success)
            {
                return false;
            }

            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                MasterId = (await _scheduleRepository.GetAvailableSlots(slotId)).First().MasterId,
                ClientId = clientId,
                AppointmentDate = (await _scheduleRepository.GetAvailableSlots(slotId)).First().AvailableFrom
            };

            await _appointmentRepository.CreateAppointment(appointment);
            return true;
        }
    }
}
