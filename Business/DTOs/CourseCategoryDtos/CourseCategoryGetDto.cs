namespace Business.DTOs.CourseCategoryDtos;

public class CourseCategoryGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
}
