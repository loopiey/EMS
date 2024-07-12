using Microsoft.EntityFrameworkCore;

public class EMSContext : DbContext
{
    public EMSContext(DbContextOptions<EMSContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public UserType UserType { get; set; }
}

public enum UserType
{
    Teacher,
    Student
}

public class Course
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Explanation { get; set; }
    public bool IsMandatory { get; set; }
    public int Credit { get; set; }
}
