using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<bool> CancelAppointmentAsync(long appointmentId);
        Task<long?> CreateAppointmentAsync(Appointment appointment);
        Task<List<Appointment?>> GetAllAppointmentsAsync();
        Task<Appointment?> GetAppointmentByIdAsync(long id);
        Task<List<Appointment>> GetAppointmentByMasterIdAsync(long masterId);
        Task<List<Appointment?>> GetAppointmentByUserIdAsync(long userId);
        Task<long?> UpdateAppointmentAsync(long id, string scheduleSlotId, string description, long masterId, long serviceId, long userId);
    }
}