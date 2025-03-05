using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAppointment();
        Task<Guid> GetAppointmentById(Guid id);
        Task<Guid> CreateAppointment(Appointment appointment);
        Task<Guid> UpdateAppointment(Guid id, DateTime appointmentDate, string description);
        Task<Guid> DeleteAppointment(Guid id);
    }
}
