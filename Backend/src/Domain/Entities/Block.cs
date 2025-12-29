using PageBuilder.Domain.Enums;

namespace PageBuilder.Domain.Entities;

public class Block
{
    public int Id { get; set; }
    public required BlockType Type { get; set; }
    public int Order { get; set; }
    public required string Content { get; set; } // JSON content
    public int PageId { get; set; }
    public required Page Page { get; set; }
}
