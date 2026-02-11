using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Services;

public class CourseService : ICourseService{private readonly ApplicationDBContext _dbContext;
    private readonly ILogger<CourseService> _logger;
 public CourseService(ApplicationDBContext dbContext, ILogger<CourseService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Response<string>> AddCourseAsync(CourseInsertDto dto)
    {
        try
        {
            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                InstructorId = dto.InstructorId
            };

            _dbContext.Courses.Add(course);
            await _dbContext.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Course added successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Add course failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<string>> DeleteAsync(int courseId)
    {
        try
        {
            var course = await _dbContext.Courses.FindAsync(courseId);

            if (course == null)
                return new Response<string>(HttpStatusCode.NotFound, "Course not found");

            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete course failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<Course>> GetCourseBuyIdAsync(int courseId)
    {
        try
        {
            var course = await _dbContext.Courses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == courseId);

            if (course == null)
                return new Response<Course>(HttpStatusCode.NotFound, "Course not found");

            return new Response<Course>(HttpStatusCode.OK, "Success", course);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get course by id failed");
            return new Response<Course>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }
    public async Task<PagedResult<Course>> GetCourse(
        CourseFilter filter,
        PagedQuery pagedQuery)
    {
        var page = pagedQuery.Page <= 0 ? 1 : pagedQuery.Page;
        var pageSize = pagedQuery.PageSize <= 0 ? 10 : pagedQuery.PageSize;

        IQueryable<Course> query = _dbContext.Courses.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter?.Title))
            query = query.Where(x => x.Title!.Contains(filter.Title));

        if (filter?.Price > 0)
            query = query.Where(x => x.Price == filter.Price);

        var totalCount = await query.CountAsync();

        var items = await query.OrderBy(x => x.Id).Skip((page - 1) * pageSize) .Take(pageSize).ToListAsync();

        return new PagedResult<Course>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }
    public async Task<Response<string>> UpdateAsync(int courseId, CourseUpdateDto courseUpdateDto)
    {
        try
        {
            var course = await _dbContext.Courses.FindAsync(courseId);

            if (course == null)
                return new Response<string>(HttpStatusCode.NotFound, "Course not found");

            course.Title = courseUpdateDto.Title;
            course.Description = courseUpdateDto.Description;
            course.Price = courseUpdateDto.Price;

            await _dbContext.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "Updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update course failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }


}
