public class StudentProfileInsertDto
{
    public int UserId { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Level { get; set; }
}
public class StudentProfileUpdateDto
{
    public int Id { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Level { get; set; }
}
public class StudentProfileGetDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Level { get; set; }
}
