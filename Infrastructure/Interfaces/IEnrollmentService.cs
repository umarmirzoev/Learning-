using System.Net;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;
public interface IEnrollmentService
{
    Task<Response<string>> AddEnrollmentAsync(EnrollmentInsertDto enrollmentInsertDto);
    Task<Response<Enrollment>> GetEnrollmentBuyIdAsync(int enrollmentId);
    Task<PagedResult<Enrollment>> GetEnrollment(EnrollmentFilter filter, PagedQuery pagedQuery);

    Task<Response<string>> UpdateAsync(int enrollmentId, EnrollmentUpdateDto enrollmentUpdateDto);
    Task<Response<string>> DeleteAsync(int enrollmentId);
}