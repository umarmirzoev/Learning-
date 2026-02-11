using System.Net;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;
public interface IInstructorProfileService
{
    Task<Response<string>> AddInstructorProfileAsync(InstructorProfileInsertDto instructorProfileInsertDto);
    Task<Response<InstructorProfile>> GetInstructorProfileBuyIdAsync(int instructorprofileId);
    Task<PagedResult<InstructorProfile>> GetInstructorProfile(InstructorProfileFiltres filter, PagedQuery pagedQuery);

    Task<Response<string>> UpdateAsync(int instructorprofileId, InstructorProfileUpdateDto instructorProfileUpdateDto);
    Task<Response<string>> DeleteAsync(int instructorprofileId);
}