using Business.DTOs.JobDtos;

namespace Business.DTOs.JobBookmarkDtos;

public class JobBookMarkGetDto
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public JobDto Job { get; set; } = null!;
    public string UserId { get; set; } = null!;
}
