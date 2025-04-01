namespace Alchemy.Domain.Models
{
    public class Service
    {
        public const int MAX_TITLE_LENGTH = 50;
        public const int MAX_DESCRIPTION_LENGTH = 255;

        private Service() { }
        private Service(Guid id, string title, string description, decimal price, int duration)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            Duration = duration;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public int Duration { get; private set; }

        public static (Service Service, string error) Create(Guid id, string title, string description, decimal price, int duration)
        {
            var error = string.Empty;
            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
            {
                error = "Title cannot be empty or longer than 50 symbols";
            }
            if (string.IsNullOrEmpty(description) || description.Length > MAX_DESCRIPTION_LENGTH)
            {
                error = "Description cannot be empty or longer than 255 symbols";
            }
            if (price <= 0)
            {
                error = "Price cannot be less or equal to 0";
            }
            if (duration <= 0)
            {
                error = "Duration cannot be less or equal to 0";
            }
            var service = new Service(id, title, description, price, duration);
            return (service, error);
        }

    }
}
