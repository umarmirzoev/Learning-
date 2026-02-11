
public class Lesson
{
    public int Id {get;set;}
    public string? Title {get;set;}
    public string? VideoUrl {get;set;}
    public int ModuleId {get;set;}
    public int DurationMinutes {get;set;}
    public Module? Module { get; set; }
}