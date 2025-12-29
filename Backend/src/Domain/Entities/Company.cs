namespace PageBuilder.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ICollection<Page> Pages { get; set; } = new List<Page>();
    }
}
