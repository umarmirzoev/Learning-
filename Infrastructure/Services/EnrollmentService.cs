using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly ApplicationDBContext _dbContext;
    private readonly ILogger<EnrollmentService> _logger;

    public EnrollmentService(ApplicationDBContext dbContext, ILogger<EnrollmentService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Response<string>> AddEnrollmentAsync(EnrollmentInsertDto dto)
    {
        try
        {
            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                EnrolledAt = DateTime.UtcNow
            };

            _dbContext.Enrollments.Add(enrollment);
            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Enrollment added successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Add Enrollment failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<string>> DeleteAsync(int enrolmentId)
    {
        try
        {
            var enrollment = await _dbContext.Enrollments.FindAsync(enrolmentId);
            if (enrollment == null) return new Response<string>(HttpStatusCode.NotFound, "Enrollment not found");

            _dbContext.Enrollments.Remove(enrollment);
            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete Enrollment failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<Enrollment>> GetEnrollmentBuyIdAsync(int enrolmentId)
    {
        try
        {
            var enrollment = await _dbContext.Enrollments.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == enrolmentId);
            if (enrollment == null) return new Response<Enrollment>(HttpStatusCode.NotFound, "Enrollment not found");

            return new Response<Enrollment>(HttpStatusCode.OK, "Success", enrollment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get Enrollment by enrolmentId failed");
            return new Response<Enrollment>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<PagedResult<Enrollment>> GetEnrollment(EnrollmentFilter filter, PagedQuery pagedQuery)
    {
        var page = pagedQuery.Page <= 0 ? 1 : pagedQuery.Page;
        var pageSize = pagedQuery.PageSize <= 0 ? 10 : pagedQuery.PageSize;

        IQueryable<Enrollment> query = _dbContext.Enrollments.AsNoTracking();

        if (filter?.StudentId > 0)
            query = query.Where(x => x.StudentId == filter.StudentId);

        if (filter?.CourseId > 0)
            query = query.Where(x => x.CourseId == filter.CourseId);

        var totalCount = await query.CountAsync();
        var items = await query.OrderBy(x => x.EnrolledAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<Enrollment>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public async Task<Response<string>> UpdateAsync(int enrolmentId, EnrollmentUpdateDto enrollmentUpdateDto)
    {
        try
        {
            var enrollment = await _dbContext.Enrollments.FindAsync(enrolmentId);
            if (enrollment == null) return new Response<string>(HttpStatusCode.NotFound, "Enrollment not found");

            enrollment.StudentId = enrollment.StudentId;
            enrollment.CourseId = enrollment.CourseId;

            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update Enrollment failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }






}
