
public class User
{
    public int Id {get;set;}
    public string? Fullname {get;set;}
    public string? Email {get;set;}
    public string? PasswordHash {get;set;}
    public DateTime CreatedAt {get;set;}
    public StudentProfile? StudentProfile { get; set; }
    public InstructorProfile? InstructorProfile { get; set; }
}