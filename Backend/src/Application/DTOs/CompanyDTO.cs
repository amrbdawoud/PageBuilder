namespace PageBuilder.Application.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateCompanyDTO
    {
        public string Name { get; set; }
    }

    public class UpdateCompanyDTO
    {
        public string Name { get; set; }
    }

    public class CompanyWithPagesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<PageDTO> Pages { get; set; } = new List<PageDTO>();
    }
}
