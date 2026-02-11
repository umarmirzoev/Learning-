using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Services;

public class CourseProgressService : ICourseProgressService
{
    private readonly ApplicationDBContext _dbContext;
    private readonly ILogger<CourseProgressService> _logger;

    public CourseProgressService(ApplicationDBContext dbContext, ILogger<CourseProgressService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Response<string>> AddCourseProgressAsync(CourseProgressInsertDto courseProgressInsertDto)
    {
        try
        {
            var progress = new CourseProgress
            {
                StudentId = courseProgressInsertDto.StudentId,
                CourseId = courseProgressInsertDto.CourseId
            };

            _dbContext.CourseProgresses.Add(progress);
            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "CourseProgress added successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Add CourseProgress failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<string>> DeleteAsync(int courseprogressId)
    {
        try
        {
            var progress = await _dbContext.CourseProgresses.FindAsync(courseprogressId);
            if (progress == null) return new Response<string>(HttpStatusCode.NotFound, "CourseProgress not found");

            _dbContext.CourseProgresses.Remove(progress);
            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete CourseProgress failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<CourseProgress>> GetCourseProgressBuyIdAsync(int courseprogressId)
    {
        try
        {
            var progress = await _dbContext.CourseProgresses.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == courseprogressId);
            if (progress == null) return new Response<CourseProgress>(HttpStatusCode.NotFound, "CourseProgress not found");

            return new Response<CourseProgress>(HttpStatusCode.OK, "Success", progress);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get CourseProgress by courseprogressId failed");
            return new Response<CourseProgress>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<PagedResult<CourseProgress>>  GetCourseProgress(CourseProgressFilter filter, PagedQuery pagedQuery)
    {
        var page = pagedQuery.Page <= 0 ? 1 : pagedQuery.Page;
        var pageSize = pagedQuery.PageSize <= 0 ? 10 : pagedQuery.PageSize;

        IQueryable<CourseProgress> query = _dbContext.CourseProgresses.AsNoTracking();

        if (filter?.StudentId > 0)
            query = query.Where(x => x.StudentId == filter.StudentId);

        if (filter?.CourseId > 0)
            query = query.Where(x => x.CourseId == filter.CourseId);

        var totalCount = await query.CountAsync();
        var items = await query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedResult<CourseProgress>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public async Task<Response<string>> UpdateAsync(int courseprogressId, CourseProgressUpdateDto dto)
    {
        try
        {
            var progress = await _dbContext.CourseProgresses.FindAsync(courseprogressId);
            if (progress == null) return new Response<string>(HttpStatusCode.NotFound, "CourseProgress not found");

            progress.CompletedLessons = dto.CompletedLessons;
            progress.IsCompleted = dto.IsCompleted;

            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update CourseProgress failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }





}
