using Alchemy.Domain.Models;
using Alchemy.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Alchemy.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMasterScheduleRepository _masterScheduleRepository;
        private readonly IMasterRepository _masterRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly UserManager<User> _userManager;
        
        private static readonly TimeSpan MinimumBookingLeadTime = TimeSpan.FromHours(24);
        private const int MaximumBookingAdvanceDays = 30;

        public AppointmentService(IAppointmentRepository appointmentRepository,
            IMasterScheduleRepository masterScheduleRepository, IMasterRepository masterRepository,
            IServiceRepository serviceRepository, UserManager<User> userManager)
        {
            _appointmentRepository = appointmentRepository;
            _masterScheduleRepository = masterScheduleRepository;
            _masterRepository = masterRepository;
            _serviceRepository = serviceRepository;
            _userManager = userManager;
        }

        public Task<Appointment?> GetAppointmentById(long id)
        {
            return _appointmentRepository.GetAppointmentById(id);
        }

        public Task<List<Appointment>> GetAllAppointments()
        {
            return _appointmentRepository.GetAllAppointments();
        }

        public Task<List<Appointment>> GetAppointmentsByUserId(string userId)
        {
            return _appointmentRepository.GetAppointmentByUserId(userId);
        }

        public Task<List<Appointment>> GetAppointmentsByMasterId(long masterId)
        {
            return _appointmentRepository.GetAppointmentByMasterId(masterId);
        }

        public async Task<(long? AppointmentId, string? Error)> CreateAppointment(long scheduleSlotId,
            string description,
            long masterId,
            long serviceId,
            string currentUserId)
        {
            var scheduleSlot = await _masterScheduleRepository.GetMasterScheduleById(scheduleSlotId);

            if (scheduleSlot == null)
                return (null, "Slot not found.");

            if (scheduleSlot.IsBooked)
                return (null, "Slot is already booked.");

            var master = await _masterRepository.GetMasterById(masterId);

            if (master == null)
                return (null, "Master not found.");

            var service = await _serviceRepository.GetServiceById(serviceId);

            if (service == null)
                return (null, "Service not found.");

            var user = await _userManager.FindByIdAsync(currentUserId);

            if (user == null)
                return (null, "User not found");

            var now = DateTime.UtcNow;
            if (scheduleSlot.SlotTime <= now.Add(MinimumBookingLeadTime))
                return (null,
                    $"Appointment must be booked at least {MinimumBookingLeadTime.TotalHours} hours in advance");

            if (scheduleSlot.SlotTime > now.AddDays(MaximumBookingAdvanceDays))
                return (null, $"Appointment cannot be booked more than {MaximumBookingAdvanceDays} days in advance.");

            var (appointment, error) = Appointment.Create(
                scheduleSlotId,
                description,
                currentUserId,
                masterId,
                serviceId,
                scheduleSlot,
                user,
                master,
                service
            );

            if (error != null)
                return (null, error);

            var createdId = await _appointmentRepository.CreateAppointment(appointment);
            return (createdId, null);
        }

        public async Task<(bool Success, string? Error)> UpdateAppointment(long appointmentId, string newDescription, string currentUserId)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(appointmentId);
            
            if (appointment == null)
                return (false, "Appointment not found.");

            if (appointment.UserId != currentUserId)
                return (false, "You don't have permission to update this appointment.");

            var (isUpdateSuccessful, error) = appointment.UpdateDescription(newDescription);
            if (!isUpdateSuccessful)
                return (false, error);

            var succes = await _appointmentRepository.UpdateAppointment(appointment);
            return (succes, succes ? null : "Appointment successfully updated");
        }

        public async Task<(bool Success, string? Error)> CancelAppointment(long appointmentId, string currentUserId, bool isUserAdmin)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(appointmentId);

            if (appointment == null)
                return (false, "Appointment not found.");

            if (appointment.UserId != currentUserId && !isUserAdmin)
                return (false, "You don't have permission to cancel this appointment");

            var success = await _appointmentRepository.DeleteAppointment(appointmentId);
            return (success, success ? null : "Failed to cancel appointment.");
        }
    }
}
