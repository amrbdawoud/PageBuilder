using PageBuilder.Domain.Enums;

namespace PageBuilder.Application.DTOs
{
    public class PageDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public PageStatus PageStatus { get; set; }
        public int CompanyId { get; set; }
    }

    public class CreatePageDTO
    {
        public required string Title { get; set; }
        public int CompanyId { get; set; }
    }

    public class UpdatePageDTO
    {
        public required string Title { get; set; }
        public PageStatus PageStatus { get; set; }
    }

    public class PageWithBlocksDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public PageStatus PageStatus { get; set; }
        public int CompanyId { get; set; }
        public List<BlockDTO> Blocks { get; set; } = new List<BlockDTO>();
    }
}
