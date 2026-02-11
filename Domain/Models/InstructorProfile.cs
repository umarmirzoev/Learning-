

public class InstructorProfile
{
    public int Id {get;set;}
    public int UserId {get;set;}
    public string? Bio {get;set;}
    public int ExperienceYears {get;set;}
     public User? User { get; set; }
    public ICollection<Course>? Courses { get; set; }
}