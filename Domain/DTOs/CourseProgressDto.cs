public class CourseProgressInsertDto
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
}
public class CourseProgressUpdateDto
{
    public int Id { get; set; }
    public int CompletedLessons { get; set; }
    public bool IsCompleted { get; set; }
}
public class CourseProgressGetDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public int CompletedLessons { get; set; }
    public bool IsCompleted { get; set; }
}
