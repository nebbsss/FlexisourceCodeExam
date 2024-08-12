namespace App.Models.Entities;

public class BaseEntity
{
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    public bool IsActive { get; set; } = true;
}

