using System.Net.NetworkInformation;

public class Course
{
    public int Id {get;set;}
    public string? Title {get;set;}
    public string? Description {get;set;}
    public decimal Price {get;set;}
    public int InstructorId {get;set;}
    public InstructorProfile? Instructor { get; set; }
    public ICollection<Module>? Modules { get; set; }
    public ICollection<Enrollment>? Enrollments { get; set; }

}