using Alchemy.Domain.Models;
namespace Alchemy.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<long?> CreateAppointmentAsync(Appointment appointment);
        Task<Appointment?> GetAllAppointmentsAsync();
        Task<Appointment?> GetAppointmentByIdAsync(long id);
        Task<List<Appointment>> GetAppointmentByUserIdAsync(long userId);
        Task<List<Appointment>> GetAppointmentByMasterIdAsync(long masterId);
        Task<bool> CancelAppointmentAsync(long appointmentId);
    }
}
