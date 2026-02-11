using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Services;

public class StudentProfileService : IStudentProfileService
{
    private readonly ApplicationDBContext _dbContext;
    private readonly ILogger<StudentProfileService> _logger;

    public StudentProfileService(ApplicationDBContext dbContext, ILogger<StudentProfileService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Response<string>> AddStudentProfileAsync(StudentProfileInsertDto dto)
    {
        try
        {
            var student = new StudentProfile
            {
                UserId = dto.UserId,
                BirthDate = dto.BirthDate,
                Level = dto.Level
            };

            _dbContext.StudentProfiles.Add(student);
            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "StudentProfile added successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Add StudentProfile failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        try
        {
            var student = await _dbContext.StudentProfiles.FindAsync(id);
            if (student == null) return new Response<string>(HttpStatusCode.NotFound, "StudentProfile not found");

            _dbContext.StudentProfiles.Remove(student);
            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete StudentProfile failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<StudentProfile>>  GetStudentProfileBuyIdAsync(int studentprofileId)
    {
        try
        {
            var student = await _dbContext.StudentProfiles.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == studentprofileId);
            if (student == null) return new Response<StudentProfile>(HttpStatusCode.NotFound, "StudentProfile not found");

            return new Response<StudentProfile>(HttpStatusCode.OK, "Success", student);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get StudentProfile by id failed");
            return new Response<StudentProfile>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<PagedResult<StudentProfile>> GetStudentProfile(StudentProfileFiltres filter, PagedQuery pagedQuery)
    {
        var page = pagedQuery.Page <= 0 ? 1 : pagedQuery.Page;
        var pageSize = pagedQuery.PageSize <= 0 ? 10 : pagedQuery.PageSize;

        IQueryable<StudentProfile> query = _dbContext.StudentProfiles.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter?.Level))
            query = query.Where(x => x.Level!.Contains(filter.Level));

        var totalCount = await query.CountAsync();
        var items = await query.OrderBy(x => x.Id)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();

        return new PagedResult<StudentProfile>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public async Task<Response<string>> UpdateAsync(int id, StudentProfileUpdateDto dto)
    {
        try
        {
            var student = await _dbContext.StudentProfiles.FindAsync(id);
            if (student == null) return new Response<string>(HttpStatusCode.NotFound, "StudentProfile not found");

            student.BirthDate = dto.BirthDate;
            student.Level = dto.Level;

            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update StudentProfile failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }
}
