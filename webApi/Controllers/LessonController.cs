using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController(ILessonService lessonService ) : ControllerBase
    {
        [HttpPost]
        public async Task<Response<string>> AddAsync(LessonInsertDto lessonInsertDto)
        {
            return await lessonService. AddLessonAsync(lessonInsertDto);
        }


        [HttpPut("{LessonId}")]
    public async Task<Response<string>> UpdateAsync(int LessonId,LessonUpdateDto Lesson)
    {
        return await lessonService.UpdateAsync(LessonId,Lesson);
    }

        [HttpDelete("{lessonId}")]
        public async Task<Response<string>> Delete(int lessonId)
        {
            return await lessonService.DeleteAsync(lessonId);
        }

        [HttpGet]
        public async Task<PagedResult<Lesson>> GetLesson([FromQuery] LessonFiltres filter, [FromQuery] PagedQuery pagedQuery)
        {
            return await lessonService.GetLesson(filter, pagedQuery);   
        }

        [HttpGet("{lessonId}")]
        public async Task<Response<Lesson>> GetLessonById(int lessonId)
        {
            return await lessonService.GetLessonBuyIdAsync(lessonId);
        }
    }
}