namespace Business.DTOs.UserDtos;

public class UserDetailDto
{
    public string Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? ProfilePhoto { get; set; }
    public string? CoverImage { get; set; }
    public bool IsActive { get; set; }
    public List<string> Roles { get; set; }
}
