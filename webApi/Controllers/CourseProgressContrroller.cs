using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseProgressController(ICourseProgressService courseProgressService ) : ControllerBase
    {
        [HttpPost]
        public async Task<Response<string>> AddAsync(CourseProgressInsertDto courseProgressInsertDto)
        {
            return await courseProgressService. AddCourseProgressAsync(courseProgressInsertDto);
        }


    [HttpPut("{CourseProgressId}")]
    public async Task<Response<string>> UpdateAsync(int CourseProgressId,CourseProgressUpdateDto course)
    {
        return await courseProgressService.UpdateAsync(CourseProgressId,course);
    }

        [HttpDelete("{courseprogressId}")]
        public async Task<Response<string>> Delete(int courseprogressId)
        {
            return await courseProgressService.DeleteAsync(courseprogressId);
        }

        [HttpGet]
        public async Task<PagedResult<CourseProgress>> GetCourseProgress([FromQuery] CourseProgressFilter filter, [FromQuery] PagedQuery pagedQuery)
        {
            return await courseProgressService.GetCourseProgress(filter, pagedQuery);   
        }

        [HttpGet("{courseprogressId}")]
        public async Task<Response<CourseProgress>> GetCourseProgressById(int courseprogressId)
        {
            return await courseProgressService.GetCourseProgressBuyIdAsync(courseprogressId);
        }
    }
}