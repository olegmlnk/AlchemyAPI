using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ZstdSharp.Unsafe;

namespace Alchemy.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AlchemyDbContext _context;
        private readonly IMapper _mapper;

        public AppointmentRepository(AlchemyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            var appointmentEntities = await _context.Appointments
                .AsNoTracking()
                .Include(a => a.ScheduleSlot)
                .Include(a => a.Master)
                .Include(a => a.User)
                .Include(a => a.Service)
                .ToListAsync();

            return _mapper.Map<List<Appointment>>(appointmentEntities);
        }

        public async Task<Appointment> GetAppointmentById(long id)
        {
            var appointmentEntity = await _context.Appointments
                .AsNoTracking()
                .Include(a => a.User)
                .Include(a => a.Master)
                .Include(a => a.Service)
                .Include(a => a.ScheduleSlot)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointmentEntity == null)
                throw new KeyNotFoundException("Appointment not found");
            

            return _mapper.Map<Appointment>(appointmentEntity);
        }

        public async Task<List<Appointment>> GetAppointmentByMasterId(long masterId)
        {
            var appointmentEntity = await _context.Appointments
                .Where(a => a.MasterId == masterId)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<Appointment>>(appointmentEntity);
        }

        public async Task<List<Appointment>> GetAppointmentByUserId(string userId)
        {
            var appointmentEntities = await _context.Appointments
                .AsNoTracking()
                .Where(a => a.UserId == userId)
                .Include(a => a.ScheduleSlot)
                .Include(a => a.Master)
                .Include(a => a.Service)
                .ToListAsync();

            if (appointmentEntities == null)
                throw new KeyNotFoundException("No appointments found.");

            return _mapper.Map<List<Appointment>>(appointmentEntities);
        }

        public async Task<long> CreateAppointment(Appointment appointment)
        {
            var appointmentEntity = _mapper.Map<Appointment>(appointment);

            var scheduleSlotEntity = await _context.MasterSchedules.FindAsync(appointment.ScheduleSlotId);

            if (scheduleSlotEntity == null)
                throw new KeyNotFoundException("Associated schedule slot not found.");

            if (scheduleSlotEntity.IsBooked)
                throw new InvalidOperationException("Schedule slot is already booked.");

            scheduleSlotEntity.IsBooked = true;

            await _context.Appointments.AddAsync(appointmentEntity);
            await _context.SaveChangesAsync();

            return appointmentEntity.Id;
        }

        public async Task<bool> UpdateAppointment(Appointment appointment)
        {
            var appointmentToUpdate = await _context.Appointments.FindAsync(appointment.Id);

            if (appointmentToUpdate == null)
                return false;

            _mapper.Map(appointment, appointmentToUpdate);

            _context.Appointments.Update(appointmentToUpdate);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAppointment(long Id)
        {
            var appointmentToDelete = await _context.Appointments
                .Include(a => a.ScheduleSlot)
                .FirstOrDefaultAsync(a => a.Id == Id);

            if (appointmentToDelete == null)
                return false;

            if (appointmentToDelete.ScheduleSlot != null)
                appointmentToDelete.ScheduleSlot.IsBooked = false;

            _context.Appointments.Remove(appointmentToDelete);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
