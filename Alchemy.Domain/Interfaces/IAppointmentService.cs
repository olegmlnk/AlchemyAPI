using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IAppointmentService
    {
        Task<Guid> CreateAppointment(Appointment appointment);
        Task<Guid> DeleteAppointment(Guid id);
        Task<List<Appointment>> GetAppointment();
        Task<Guid> GetAppointmentById(Guid id);
        Task<Guid> UpdateAppointment(Guid id, DateTime appointmentDate, string description);
        Task<List<MasterSchedule>> GetAvailableSlots(Guid masterId);
        Task<bool> BookAppointment(Guid slotId, Guid clientId);
    }
}