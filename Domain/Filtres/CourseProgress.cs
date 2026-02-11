using System;

namespace WebApi.Filters;

public class CourseProgressFilter
{
    public int CourseId {get;set;}
    public int StudentId {get;set;}
    public int CompletedLessons {get;set;}
    public Boolean IsCompleted {get;set;}
}