﻿using Alchemy.Infrastructure.Entities;
using Alchemy.Domain.Interfaces;
using Alchemy.Domain.Models;
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

        public async Task<List<Appointment>> GetAppointment()
        {
            var appointmentEntities = await _context.Appointments
                .AsNoTracking()
                .ToListAsync();

            var appointments = appointmentEntities
                .Select(a => Appointment.Create(a.Id, a.AppointmentDate, a.Description, a.MasterId, a.ServiceId, a.UserId).Appointment)
                .ToList();

            return appointments;
        }


        public async Task<long> GetAppointmentById(long id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
                throw new KeyNotFoundException("Appointment not found");

            return appointment.Id;
        }

        public async Task<long> UpdateAppointment(long id, DateTime appointmentDate, string description, long masterId, long serviceId, long userId)
        {
            await _context.Appointments
                .Where(a => a.Id == id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(a => a.AppointmentDate, a => appointmentDate)
                .SetProperty(a => a.Description, a => description)
                .SetProperty(a => a.MasterId, a => masterId)
                .SetProperty(a => a.ServiceId, a => serviceId)
                );

            return id;
        }

        public async Task<long> CreateAppointment(Appointment appointment)
        {
            var appointmentEntity = new AppointmentEntity
            {
                Id = appointment.Id,
                AppointmentDate = appointment.AppointmentDate,
                UserId = appointment.UserId,
                MasterId = appointment.MasterId,
                ServiceId = appointment.ServiceId,
                Description = appointment.Description
            };

            await _context.Appointments.AddAsync(appointmentEntity);
            await _context.SaveChangesAsync();

            return appointment.Id;
        }

        public async Task<long> DeleteAppointment(long id)
        {
            await _context.Appointments
                .Where (a => a.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

    }
}
