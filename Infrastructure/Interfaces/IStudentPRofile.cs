using System.Net;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;
public interface IStudentProfileService
{
    Task<Response<string>> AddStudentProfileAsync(StudentProfileInsertDto studentProfileInsertDto);
    Task<Response<StudentProfile>> GetStudentProfileBuyIdAsync(int studentprofileId);
    Task<PagedResult<StudentProfile>> GetStudentProfile(StudentProfileFiltres filter, PagedQuery pagedQuery);

    Task<Response<string>> UpdateAsync(int studentprofileId, StudentProfileUpdateDto studentProfileUpdateDto);
    Task<Response<string>> DeleteAsync(int studentprofileId);
}