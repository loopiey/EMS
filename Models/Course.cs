using EMS.API.Models;

public class Course
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Explanation { get; set; }
    public bool IsMandatory { get; set; }
    public int Credit { get; set; } // Maximum 6
    public int TeacherId { get; set; } // Foreign key
    public User? Teacher { get; set; }
}
