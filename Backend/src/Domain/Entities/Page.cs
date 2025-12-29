using PageBuilder.Domain.Enums;

namespace PageBuilder.Domain.Entities
{
    public class Page
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public PageStatus PageStatus { get; set; } = PageStatus.Draft;

        public int CompanyId { get; set; }
        public int? SalesRepId { get; set; }
        public required Company Company { get; set; }
        public ICollection<Block> Blocks { get; set; } = new List<Block>();
    }
}
