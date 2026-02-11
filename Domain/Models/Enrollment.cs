
public class Enrollment
{
    public int Id {get;set;}
    public int StudentId {get;set;}
    public int CourseId {get;set;}
    public DateTime EnrolledAt {get;set;}
     public StudentProfile? Student { get; set; }
    public Course? Course { get; set; }
}