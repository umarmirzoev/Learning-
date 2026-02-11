using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorProfileController( IInstructorProfileService  instructorProfileService) : ControllerBase
    {
        [HttpPost]
        public async Task<Response<string>> AddAsync(InstructorProfileInsertDto instructorProfileInsertDto)
        {
            return await instructorProfileService. AddInstructorProfileAsync(instructorProfileInsertDto);
        }


     [HttpPut("{InstructorProfileId}")]
    public async Task<Response<string>> UpdateAsync(int InstructorProfileId,InstructorProfileUpdateDto instructorProfile)
    {
        return await instructorProfileService.UpdateAsync(InstructorProfileId,instructorProfile);
    }

        [HttpDelete("{instructorprofileId}")]
        public async Task<Response<string>> Delete(int instructorprofileId)
        {
            return await instructorProfileService.DeleteAsync(instructorprofileId);
        }

        [HttpGet]
        public async Task<PagedResult<InstructorProfile>> GetInstructorProfile([FromQuery] InstructorProfileFiltres filter, [FromQuery] PagedQuery pagedQuery)
        {
            return await instructorProfileService.GetInstructorProfile(filter, pagedQuery);   
        }

        [HttpGet("{instructorprofileId}")]
        public async Task<Response<InstructorProfile>> GetInstructorProfileById(int instructorprofileId)
        {
            return await instructorProfileService.GetInstructorProfileBuyIdAsync(instructorprofileId);
        }
    }
}