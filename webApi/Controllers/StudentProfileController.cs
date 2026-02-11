using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentProfileController(IStudentProfileService studentProfileService  ) : ControllerBase
    {
        [HttpPost]
        public async Task<Response<string>> AddAsync(StudentProfileInsertDto studentProfileInsertDto )
        {
            return await studentProfileService. AddStudentProfileAsync(studentProfileInsertDto);
        }

 
     [HttpPut("{studentId}")]
    public async Task<Response<string>> UpdateAsync(int studentprofileId, StudentProfileUpdateDto StudentProfile)
    {
        return await studentProfileService.UpdateAsync(studentprofileId,StudentProfile);
    }

        [HttpDelete("{studentId}")]
        public async Task<Response<string>> Delete(int studentId)
        {
            return await studentProfileService.DeleteAsync(studentId);
        }

        [HttpGet]
        public async Task<PagedResult<StudentProfile>> GetStudentProfile([FromQuery] StudentProfileFiltres filter, [FromQuery] PagedQuery pagedQuery)
        {
            return await studentProfileService.GetStudentProfile(filter, pagedQuery);   
        }

        [HttpGet("{studentId}")]
        public async Task<Response<StudentProfile>> GetStudentProfielById(int studentId)
        {
            return await studentProfileService.GetStudentProfileBuyIdAsync(studentId);
        }
    }
}