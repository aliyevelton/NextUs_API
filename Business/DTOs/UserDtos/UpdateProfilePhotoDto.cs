using Microsoft.AspNetCore.Http;

namespace Business.DTOs.UserDtos;

public class UpdateProfilePhotoDto
{
    public IFormFile ProfilePhotoFile { get; set; }
}
