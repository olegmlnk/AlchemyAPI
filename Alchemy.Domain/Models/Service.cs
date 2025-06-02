using System.ComponentModel.DataAnnotations;

namespace Alchemy.Domain.Models
{
    public class Service
    {
        public const int MAX_TITLE_LENGTH = 50;
        public const int MAX_DESCRIPTION_LENGTH = 255;

        private Service()
        {
        }

        private Service(long id, string title, string description, double price, double duration)
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
        public double Duration { get; private set; }

        public static Service Create(long id, string title, string description, double price,
            double duration)
        {
            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
            {
                throw new ValidationException("Title cannot be empty or more that 50 symbols!");
            }
            if (string.IsNullOrEmpty(description) || title.Length > MAX_DESCRIPTION_LENGTH)
            {
                throw new ValidationException("Description cannot be empty or more that 250 symbols!");
            }
            if (price <= 0)
            {
                throw new ValidationException("Price cannot be less or equal to zero!");
            }

            return new Service(id, title, description, price, duration);
        }

    }
}

