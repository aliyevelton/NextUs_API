namespace Business.DTOs.CategoryDtos;

public class JobCategoryGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
}
