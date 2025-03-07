namespace Alchemy.Infrastructure.Entities
{
    public class ServiceEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } 
        public int Duration { get; set; } 
    }
}
