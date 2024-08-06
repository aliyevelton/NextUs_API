namespace Business.DTOs.JobBookmarkDtos;

public class JobBookmarkDto
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public string UserId { get; set; } = null!;
}
