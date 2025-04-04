using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IAppointmentService
    {
        Task<long> CreateAppointment(Appointment appointment);
        Task<long> DeleteAppointment(long id);
        Task<List<Appointment>> GetAppointment();
        Task<long> GetAppointmentById(long id);
        Task<long> UpdateAppointment(long id, DateTime appointmentDate, string description, long masterId, long serviceId, long userId);
        Task<List<MasterSchedule>> GetAvailableSlots(long masterId);
        Task<bool> BookAppointment(long slotId, long clientId);
    }
}