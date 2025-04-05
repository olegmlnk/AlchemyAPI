namespace Alchemy.Infrastructure.Entities
{
    public class ServiceEntity
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } 
        public double Duration { get; set; } 
    }
}
