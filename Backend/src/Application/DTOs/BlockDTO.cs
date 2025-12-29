using PageBuilder.Domain.Enums;

namespace PageBuilder.Application.DTOs
{
    public class BlockDTO
    {
        public int Id { get; set; }
        public required BlockType Type { get; set; }
        public int Order { get; set; }
        public required string Content { get; set; }
        public int PageId { get; set; }
    }

    public class CreateBlockDTO
    {
        public required BlockType Type { get; set; }
        public int Order { get; set; }
        public required string Content { get; set; }
        public int PageId { get; set; }
    }

    public class UpdateBlockDTO
    {
        public required BlockType Type { get; set; }
        public int Order { get; set; }
        public required string Content { get; set; }
    }

    public class ReorderBlocksDTO
    {
        public List<int> BlockIds { get; set; } = new List<int>();
    }
}
