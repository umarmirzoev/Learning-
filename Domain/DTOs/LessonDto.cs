public class LessonInsertDto
{
    public string? Title { get; set; }
    public string? VideoUrl { get; set; }
    public int ModuleId { get; set; }
    public int DurationMinutes { get; set; }
}
public class LessonUpdateDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? VideoUrl { get; set; }
    public int DurationMinutes { get; set; }
}
public class LessonGetDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? VideoUrl { get; set; }
    public int ModuleId { get; set; }
    public int DurationMinutes { get; set; }
}
