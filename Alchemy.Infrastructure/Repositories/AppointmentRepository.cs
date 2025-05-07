using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
using Alchemy.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
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

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            var appointmentEntities = await _context.Appointments
                .AsNoTracking()
                .ToListAsync();

            var appointments = appointmentEntities
                .Select(a => Appointment.Create(
                    a.ScheduleSlotId,
                    a.Description,
                    a.MasterId,
                    a.ServiceId,
                    a.UserId).Appointment).ToList();

            return appointments;
        }

        [Authorize(Roles = "Admin")]
        public async Task<Appointment> GetAppointmentByIdAsync(long id)
        {
            var appointmentEntity = await _context.Appointments.FindAsync(id);

            if (appointmentEntity == null)
                throw new KeyNotFoundException("Appointment not found");

            var appointment = Appointment.Create(
                appointmentEntity.ScheduleSlotId,
                appointmentEntity.Description,
                appointmentEntity.UserId,
                appointmentEntity.MasterId,
                appointmentEntity.ServiceId).Appointment;

            return appointment;
        }

        [Authorize(Roles = "Admin")]
        public async Task<List<Appointment>> GetAppointmentByMasterIdAsync(long masterId)
        {
            var appointmentEntity = await _context.Appointments
                .Where(a => a.MasterId == masterId)
                .AsNoTracking()
                .ToListAsync();

            var apointments = appointmentEntity
                .Select(a => Appointment.Create(
                    a.ScheduleSlotId,
                    a.Description,
                    a.MasterId,
                    a.ServiceId,
                    a.UserId).Appointment).ToList();

            return apointments;
        }

        [Authorize(Roles = "Admin")]
        public async Task<List<Appointment>> GetAppointmentByUserIdAsync(long userId)
        {
            var appointmentEntity = await _context.Appointments
                 .Where(a => a.UserId == userId)
                 .AsNoTracking()
                 .ToListAsync();

            var apointments = appointmentEntity
                .Select(a => Appointment.Create(
                    a.ScheduleSlotId,
                    a.Description,
                    a.MasterId,
                    a.ServiceId,
                    a.UserId).Appointment).ToList();

            return apointments;
        }

        public async Task<long?> CreateAppointmentAsync(Appointment appointment)
        {
            var scheduleSlot = await _context.MasterSchedules
                .FirstOrDefaultAsync(s => s.Id == appointment.ScheduleSlotId);

            if (scheduleSlot == null)
                throw new KeyNotFoundException("Schedule slot not found");

            if (scheduleSlot.IsBooked)
                throw new InvalidOperationException("Schedule slot is already booked");

            scheduleSlot.IsBooked = true;
            _context.MasterSchedules.Update(scheduleSlot);

            var appointmentEntity = new AppointmentEntity
            {
                ScheduleSlotId = appointment.ScheduleSlotId,
                Description = appointment.Description,
                MasterId = appointment.MasterId,
                ServiceId = appointment.ServiceId,
                UserId = appointment.UserId
            };

            await _context.Appointments.AddAsync(appointmentEntity);

            await _context.SaveChangesAsync();

            return appointmentEntity.Id;
        }

        public async Task<long?> UpdateAppointmentAsync(long id, string scheduleSlotId, string description,  long masterId,long  serviceId, long userId)
        {
            var appointmentEntity = await _context.Appointments.FindAsync(id);

            if (appointmentEntity == null)
                throw new KeyNotFoundException("Appointment not found");

            appointmentEntity.ScheduleSlotId = long.Parse(scheduleSlotId);
            appointmentEntity.Description = description;
            appointmentEntity.MasterId = masterId;
            appointmentEntity.ServiceId = serviceId;
            appointmentEntity.UserId = userId;

            _context.Appointments.Update(appointmentEntity);

            var result = await _context.SaveChangesAsync();

            return result > 0 ? appointmentEntity.Id : null;
        }

        public async Task<bool> CancelAppointmentAsync(long appointmentId)
        {
            var appointmentEntity = await _context.Appointments.FindAsync(appointmentId);

            if (appointmentEntity == null)
                throw new KeyNotFoundException("Appointment not found");

            _context.Appointments.Remove(appointmentEntity);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

    }
}
