
public class CourseProgress
{
    public int Id {get;set;}
    public int StudentId {get;set;}
    public int CourseId {get;set;}
    public int CompletedLessons {get;set;}
    public Boolean IsCompleted {get;set;}
     public StudentProfile? Student { get; set; }
    public Course? Course { get; set;}
}