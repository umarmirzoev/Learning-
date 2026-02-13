using Microsoft.AspNetCore.Identity;
public class User : IdentityUser
{
    public int Id {get;set;}
    public string? Fullname {get;set;}
    public string? Email {get;set;}
    public string? PasswordHash {get;set;}
    public DateTime CreatedAt {get;set;}
    public StudentProfile? StudentProfile { get; set; }
    public InstructorProfile? InstructorProfile { get; set; }
}