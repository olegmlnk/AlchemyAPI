﻿namespace Alchemy.Domain.Models
{
    public class Master
    {
        public const int MAX_DESCRIPTION_LENGTH = 100;
        public const int MAX_EXPEIRENCE_LENGTH = 25;

        private Master() { }
        public Master(string name, string experience, string description, List<Appointment> appointments)
        {
            Name = name;
            Experience = experience;
            Description = description;
            Appointments = appointments;
        }

        public long Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Experience { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public List<Appointment> Appointments { get; private set; }

        public static (Master master, string error) Create(string name, string experience, string description, List<Appointment> appointments)
        {
            var error = string.Empty;

            if(string.IsNullOrEmpty(description) || description.Length > MAX_DESCRIPTION_LENGTH)
            {
                Console.WriteLine("Description cannot be empty or er than 100 symbols length");
            }

            if (string.IsNullOrEmpty(experience) || experience.Length > MAX_EXPEIRENCE_LENGTH)
            {
                Console.WriteLine("Expeirence cannot be empty or er than 25 symbols length");
            }

            var master = new Master(name, experience, description, appointments);

            return (master, error);
        }
    }
}
