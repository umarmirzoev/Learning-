using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Services;

public class LessonService : ILessonService
{
    private readonly ApplicationDBContext _dbContext;
    private readonly ILogger<LessonService> _logger;

    public LessonService(ApplicationDBContext dbContext, ILogger<LessonService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Response<string>>  AddLessonAsync(LessonInsertDto dto)
    {
        try
        {
            var lesson = new Lesson
            {
                Title = dto.Title,
                VideoUrl = dto.VideoUrl,
                DurationMinutes = dto.DurationMinutes,
                ModuleId = dto.ModuleId
            };

            _dbContext.Lessons.Add(lesson);
            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Lesson added successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Add Lesson failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<string>> DeleteAsync(int lessonId)
    {
        try
        {
            var lesson = await _dbContext.Lessons.FindAsync(lessonId);
            if (lesson == null) return new Response<string>(HttpStatusCode.NotFound, "Lesson not found");

            _dbContext.Lessons.Remove(lesson);
            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete Lesson failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<Lesson>> GetLessonBuyIdAsync(int lessonId)
    {
        try
        {
            var lesson = await _dbContext.Lessons.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == lessonId);
            if (lesson == null) return new Response<Lesson>(HttpStatusCode.NotFound, "Lesson not found");

            return new Response<Lesson>(HttpStatusCode.OK, "Success", lesson);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get Lesson by lessonId failed");
            return new Response<Lesson>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<PagedResult<Lesson>>  GetLesson(LessonFiltres filter, PagedQuery pagedQuery)
    {
        var page = pagedQuery.Page <= 0 ? 1 : pagedQuery.Page;
        var pageSize = pagedQuery.PageSize <= 0 ? 10 : pagedQuery.PageSize;

        IQueryable<Lesson> query = _dbContext.Lessons.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter?.Title))
            query = query.Where(x => x.Title!.Contains(filter.Title));

        var totalCount = await query.CountAsync();
        var items = await query.OrderBy(x => x.Id)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();

        return new PagedResult<Lesson>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public async Task<Response<string>> UpdateAsync(int lessonId, LessonUpdateDto dto)
    {
        try
        {
            var lesson = await _dbContext.Lessons.FindAsync(lessonId);
            if (lesson == null) return new Response<string>(HttpStatusCode.NotFound, "Lesson not found");

            lesson.Title = dto.Title;
            lesson.VideoUrl = dto.VideoUrl;
            lesson.DurationMinutes = dto.DurationMinutes;

            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update Lesson failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }


 

}
