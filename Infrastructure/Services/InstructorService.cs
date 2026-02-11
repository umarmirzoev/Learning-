using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Services;

public class InstructorProfileService : IInstructorProfileService
{
    private readonly ApplicationDBContext _dbContext;
    private readonly ILogger<InstructorProfileService> _logger;

    public InstructorProfileService(ApplicationDBContext dbContext, ILogger<InstructorProfileService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Response<string>> AddInstructorProfileAsync(InstructorProfileInsertDto dto)
    {
        try
        {
            var instructor = new InstructorProfile
            {
                UserId = dto.UserId,
                Bio = dto.Bio,
                ExperienceYears = dto.ExperienceYears
            };

            _dbContext.InstructorProfiles.Add(instructor);
            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "InstructorProfile added successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Add InstructorProfile failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        try
        {
            var instructor = await _dbContext.InstructorProfiles.FindAsync(id);
            if (instructor == null) return new Response<string>(HttpStatusCode.NotFound, "InstructorProfile not found");

            _dbContext.InstructorProfiles.Remove(instructor);
            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete InstructorProfile failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<InstructorProfile>> GetInstructorProfileBuyIdAsync(int instructorprofileId)
    {
        try
        {
            var instructor = await _dbContext.InstructorProfiles.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == instructorprofileId);
            if (instructor == null) return new Response<InstructorProfile>(HttpStatusCode.NotFound, "InstructorProfile not found");

            return new Response<InstructorProfile>(HttpStatusCode.OK, "Success", instructor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get InstructorProfile by id failed");
            return new Response<InstructorProfile>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<PagedResult<InstructorProfile>>  GetInstructorProfile(InstructorProfileFiltres filter, PagedQuery pagedQuery)
    {
        var page = pagedQuery.Page <= 0 ? 1 : pagedQuery.Page;
        var pageSize = pagedQuery.PageSize <= 0 ? 10 : pagedQuery.PageSize;

        IQueryable<InstructorProfile> query = _dbContext.InstructorProfiles.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter?.Bio))
            query = query.Where(x => x.Bio!.Contains(filter.Bio));

        var totalCount = await query.CountAsync();
        var items = await query.OrderBy(x => x.Id)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();

        return new PagedResult<InstructorProfile>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public async Task<Response<string>> UpdateAsync(int id, InstructorProfileUpdateDto dto)
    {
        try
        {
            var instructor = await _dbContext.InstructorProfiles.FindAsync(id);
            if (instructor == null) return new Response<string>(HttpStatusCode.NotFound, "InstructorProfile not found");

            instructor.Bio = dto.Bio;
            instructor.ExperienceYears = dto.ExperienceYears;

            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update InstructorProfile failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

}

