using System.Net;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;
public interface ILessonService
{
    Task<Response<string>> AddLessonAsync(LessonInsertDto lessonInsertDto);
    Task<Response<Lesson>> GetLessonBuyIdAsync(int lessonId);
    Task<PagedResult<Lesson>> GetLesson(LessonFiltres filter, PagedQuery pagedQuery);

    Task<Response<string>> UpdateAsync(int lessonId, LessonUpdateDto lessonUpdateDto);
    Task<Response<string>> DeleteAsync(int lessonId);
}