using System.Net;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;
public interface ICourseService
{
    Task<Response<string>> AddCourseAsync(CourseInsertDto courseInsertDto);
    Task<Response<Course>> GetCourseBuyIdAsync(int courseId);
    Task<PagedResult<Course>> GetCourse(CourseFilter filter, PagedQuery pagedQuery);
    Task<Response<string>> UpdateAsync(int courseId, CourseUpdateDto courseUpdateDt);
    Task<Response<string>> DeleteAsync(int courseprogressId);
}


