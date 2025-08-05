using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IAppointmentService
    {
        Task<Appointment?> GetAppointmentById(Guid id);
        Task<List<Appointment>> GetAllAppointments();
        Task<List<Appointment>> GetAppointmentsByUserId(Guid userId);
        Task<List<Appointment>> GetAppointmentsByMasterId(Guid masterId);
        Task<(Guid? AppointmentId, string? Error)> CreateAppointment(Guid scheduleSlotId,
            string description,
            Guid masterId,
            Guid serviceId,
            Guid currentUserId);
        Task<(bool Success, string? Error)> UpdateAppointment(Guid appointmentId, string newDescription,
            Guid currentUserId);

        Task<(bool Success, string? Error)> CancelAppointment(Guid appointmentId, Guid currentUserId,
            bool isUserAdmin);
    }
}