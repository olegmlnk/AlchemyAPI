using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Security;

namespace Alchemy.Domain.Models
{
    public class Service
    {
        public const int MAX_TITLE_LENGTH = 50;
        public const int MAX_DESCRIPTION_LENGTH = 255;

        protected Service()
        {
            Title = String.Empty;
            Description = string.Empty;
        }

        private Service(long id, string title, string description, double price, TimeSpan duration)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            Duration = duration;
        }

        public long Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public double Price { get; private set; }
        public TimeSpan Duration { get; private set; }

        public static (Service service, string? Error) Create(long id, string title, string description, double price,
            TimeSpan duration)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(title))
                errors.Add("Title cannot be empty");
            else if (title.Length > MAX_TITLE_LENGTH)
                errors.Add($"Title cannot be longer than {MAX_TITLE_LENGTH} symbols");

            if (string.IsNullOrWhiteSpace(description))
                errors.Add("Description cannot be empty");
            else if (description.Length > MAX_DESCRIPTION_LENGTH)
                errors.Add($"Description cannot be longer than {MAX_DESCRIPTION_LENGTH} symbols");

            if (price <= 0)
                errors.Add("Price cannot be 0 or less.");

            if (errors.Any())
                return (null, string.Join("; ", errors));

            var service = new Service(id, title, description, price, duration);

            return (service, null);
        }

        public (bool Success, string? Error) UpdateDetails(string title, string description, double price,
            TimeSpan duration)
        {
            var errors = new List<string>();
            
            if (string.IsNullOrWhiteSpace(title))
                errors.Add("Title cannot be empty");
            else if (title.Length > MAX_TITLE_LENGTH)
                errors.Add($"Title cannot be longer than {MAX_TITLE_LENGTH} symbols");

            if (string.IsNullOrWhiteSpace(description))
                errors.Add("Description cannot be empty");
            else if (description.Length > MAX_DESCRIPTION_LENGTH)
                errors.Add($"Description cannot be longer than {MAX_DESCRIPTION_LENGTH} symbols");

            if (price <= 0)
                errors.Add("Price cannot be 0 or less.");

            if (errors.Any())
                return (false, string.Join("; ", errors));

            Title = title;
            Description = description;
            Price = price;
            Duration = duration;
            return (true, null);
        }

    }
}

