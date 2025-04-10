using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using Alchemy.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alchemy.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AlchemyDbContext _context;

        public AppointmentRepository(AlchemyDbContext context)
        {
            _context = context;
        }

        public Task<bool> CancelAppointmentAsync(long appointmentId)
        {
            throw new NotImplementedException();
        }

        public Task<long?> CreateAppointmentAsync(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment?> GetAllAppointmentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Appointment?> GetAppointmentByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointment>> GetAppointmentByMasterIdAsync(long masterId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointment>> GetAppointmentByUserIdAsync(long userId)
        {
            throw new NotImplementedException();
        }

        //public async Task<long?> CreateAppointmentAsync(Appointment appointment)
        //{
        //    var slot = await _context.MasterSchedules
        //        .FirstOrDefaultAsync(x => x.Id == appointment.ScheduleSlotId);

        //    if (slot == null || slot.IsBooked)
        //        return null;

        //    slot.IsBooked = true;

        //    var appointmentEntity = new AppointmentEntity
        //    {
        //        ScheduleSlotId = appointment.ScheduleSlotId,
        //        Description = appointment.Description,
        //        MasterId = appointment.MasterId,
        //        ServiceId = appointment.ServiceId,
        //        UserId = appointment.UserId
        //    };

        //    await _context.Appointments.AddAsync(appointmentEntity);
        //    await _context.SaveChangesAsync();

        //    return appointmentEntity.Id;
        //}

        //public async Task<Appointment?> GetAppointmentByIdAsync(long id)
        //{
        //    var entity = await _context.Appointments
        //        .Include(x => x.ScheduleSlot)
        //        .Include(x => x.Master)
        //        .Include(x => x.Service)
        //        .Include(x => x.User)
        //        .FirstOrDefaultAsync(x => x.Id == id);

        //    return entity != null ? MapToDomain(entity) : null;
        //}

        //public async Task<List<Appointment>> GetAppointmentsByMasterIdAsync(long masterId)
        //{
        //    var entities = await _context.Appointments
        //        .Include(x => x.ScheduleSlot)
        //        .Include(x => x.User)
        //        .Include(x => x.Service)
        //        .Where(x => x.MasterId == masterId)
        //        .ToListAsync();

        //    return entities.Select(MapToDomain).ToList();
        //}

        //public async Task<List<Appointment>> GetAppointmentsByUserIdAsync(long userId)
        //{
        //    var entities = await _context.Appointments
        //        .Include(x => x.ScheduleSlot)
        //        .Include(x => x.Master)
        //        .Include(x => x.Service)
        //        .Where(x => x.UserId == userId)
        //        .ToListAsync();

        //    return entities.Select(MapToDomain).ToList();
        //}

        //public async Task<bool> CancelAppointmentAsync(long appointmentId)
        //{
        //    var entity = await _context.Appointments
        //        .Include(x => x.ScheduleSlot)
        //        .FirstOrDefaultAsync(x => x.Id == appointmentId);

        //    if (entity == null)
        //        return false;

        //    entity.ScheduleSlot.IsBooked = false;

        //    _context.Appointments.Remove(entity);
        //    await _context.SaveChangesAsync();

        //    return true;
        //}

    }
}
