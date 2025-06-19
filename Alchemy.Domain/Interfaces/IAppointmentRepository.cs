using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetAppointmentById(long id);
        Task<List<Appointment>> GetAllAppointments();
        Task<List<Appointment>> GetAppointmentByUserId(string userId);
        Task<List<Appointment>> GetAppointmentByMasterId(long masterId);
        Task<long> CreateAppointment(Appointment appointment);
        Task<bool> UpdateAppointment(Appointment appointment);
        Task<bool> DeleteAppointment(long id);
    }
}