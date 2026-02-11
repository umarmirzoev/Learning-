
public class StudentProfile
{
    public int Id {get;set;}
    public int UserId {get;set;}
    public DateTime BirthDate {get;set;}
    public string? Level {get;set;}
    public User? User { get; set; }
    public ICollection<Enrollment>? Enrollments { get; set; }
    public ICollection<CourseProgress>? CourseProgresses { get; set; }

}