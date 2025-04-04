using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAppointment();
        Task<long> GetAppointmentById(long id);
        Task<long> CreateAppointment(Appointment appointment);
        Task<long> UpdateAppointment(long id, DateTime appointmentDate, string description, long masterId, long serviceId, long userId);
        Task<long> DeleteAppointment(long id);
    }
}
