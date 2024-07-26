﻿using System.ComponentModel.DataAnnotations;

namespace Business.DTOs.UserDtos;

public class UserCreateDto
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
