using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<Appointment?>> GetAllAppointmentsAsync();
        Task<Appointment?> GetAppointmentByIdAsync(long id);
        Task<List<Appointment>> GetAppointmentByMasterIdAsync(long masterId);
        Task<List<Appointment>> GetAppointmentByUserIdAsync(long userId);
        Task<long?> CreateAppointmentAsync(Appointment appointment);
        Task<long?> UpdateAppointmentAsync(long id, string scheduleSlotId, string description, long masterId, long serviceId, long userId);
        Task<bool> CancelAppointmentAsync(long appointmentId);
    }
}