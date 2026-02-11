public class EnrollmentInsertDto
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
}
public class EnrollmentUpdateDto
{
    public int Id { get; set; }
    public DateTime EnrolledAt { get; set; }
}
public class EnrollmentGetDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrolledAt { get; set; }
}
