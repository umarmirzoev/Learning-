using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(ICourseService courseService) : ControllerBase
    {
        [HttpPost]
        public async Task<Response<string>> AddAsync(CourseInsertDto courseInsertDto)
        {
            return await courseService.AddCourseAsync(courseInsertDto);
        }

        [HttpPut]
        // public async Task<Response<string>> Update(CourseUpdateDto course)
        // {
        //     return await courseService.Update(course);
        // }

        [HttpDelete("{courseId}")]
        public async Task<Response<string>> Delete(int bookId)
        {
            return await courseService.DeleteAsync(bookId);
        }

        [HttpGet]
        public async Task<PagedResult<Course>> GetCourse([FromQuery] CourseFilter filter, [FromQuery] PagedQuery pagedQuery)
        {
            return await courseService.GetCourse(filter, pagedQuery);   
        }

        [HttpGet("{courseId}")]
        public async Task<Response<Course>> GetBookById(int courseId)
        {
            return await courseService.GetCourseBuyIdAsync(courseId);
        }
    }
}