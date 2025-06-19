using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IAppointmentService
    {
        Task<Appointment?> GetAppointmentById(long id);
        Task<List<Appointment>> GetAllAppointments();
        Task<List<Appointment>> GetAppointmentsByUserId(string userId);
        Task<List<Appointment>> GetAppointmentsByMasterId(long masterId);
        Task<(long? AppointmentId, string? Error)> CreateAppointment(long scheduleSlotId,
            string description,
            long masterId,
            long serviceId,
            string currentUserId);
        Task<(bool Success, string? Error)> UpdateAppointment(long appointmentId, string newDescription,
            string currentUserId);

        Task<(bool Success, string? Error)> CancelAppointment(long appointmentId, string currentUserId,
            bool isUserAdmin);
    }
}