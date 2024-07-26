using Core.Entities.Common;

namespace Core.Entities;

public class Company : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? About { get; set; }
    public string? Logo { get; set; }
    public string? CoverImage { get; set; }
    public string? Website { get; set; }
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public ICollection<Job>? Jobs { get; set; }
}
