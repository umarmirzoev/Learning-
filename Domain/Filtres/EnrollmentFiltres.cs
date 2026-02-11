using System;

namespace WebApi.Filters;

public class EnrollmentFilter
{
    public int StudentId {get;set;}
    public int CourseId {get;set;}
    public DateTime EnrolledAt {get;set;}
}