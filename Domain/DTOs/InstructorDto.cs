public class InstructorProfileInsertDto
{
    public int UserId { get; set; }
    public string? Bio { get; set; }
    public int ExperienceYears { get; set; }
}
public class InstructorProfileUpdateDto
{
    public int Id { get; set; }
    public string? Bio { get; set; }
    public int ExperienceYears { get; set; }
}
public class InstructorProfileGetDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Bio { get; set; }
    public int ExperienceYears { get; set; }
}
