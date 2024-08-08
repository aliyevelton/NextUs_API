namespace Business.DTOs.UserDtos;

public class ChangeUserRolesDto
{
    public string UserId { get; set; }
    public List<string> Roles { get; set; }
}
