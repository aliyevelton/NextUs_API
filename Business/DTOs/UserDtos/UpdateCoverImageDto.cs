using Microsoft.AspNetCore.Http;

namespace Business.DTOs.UserDtos;

public class UpdateCoverImageDto
{
    public IFormFile CoverImageFile { get; set; }
}
