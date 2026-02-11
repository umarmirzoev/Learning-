using System.Net;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;
public interface ICourseProgressService
{
    Task<Response<string>> AddCourseProgressAsync(CourseProgressInsertDto courseProgressInsertDto);
    Task<Response<CourseProgress>> GetCourseProgressBuyIdAsync(int courseprogressId);
    Task<PagedResult<CourseProgress>> GetCourseProgress(CourseProgressFilter filter, PagedQuery pagedQuery);

    Task<Response<string>> UpdateAsync(int courseprogressId, CourseProgressUpdateDto courseProgressUpdateDto);
    Task<Response<string>> DeleteAsync(int courseprogressId);
}

