using Alchemy.Domain.Models;

namespace Alchemy.Domain.Interfaces
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
       // Task<bool> DeleteAppointment(Guid id); //TODO на подумати
    }
}