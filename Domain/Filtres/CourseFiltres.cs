using System;

namespace WebApi.Filters;

public class CourseFilter
{
    public string? Title {get; set;}=null!;
    public string? Description {get; set;}
    public decimal Price {get;set;}
    public int InstructorId {get;set;}
}