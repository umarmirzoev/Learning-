public class ModuleInsertDto
{
    public string? Title { get; set; }
    public int CourseId { get; set; }
    public int Order { get; set; }
}
public class ModuleUpdateDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int Order { get; set; }
}
public class ModuleGetDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int CourseId { get; set; }
    public int Order { get; set; }
}
