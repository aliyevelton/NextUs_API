using Core.Entities.Common;

namespace Core.Entities;

public class ContactUs : BaseEntity
{
    public string Name { get; set; }  = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Message { get; set; } = null!;
    public bool IsRead { get; set; }
}
