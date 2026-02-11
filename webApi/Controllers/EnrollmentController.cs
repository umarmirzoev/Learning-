using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController(IEnrollmentService enrollmentService ) : ControllerBase
    {
        [HttpPost]
        public async Task<Response<string>> AddAsync(EnrollmentInsertDto enrollmentInsertDto)
        {
            return await enrollmentService. AddEnrollmentAsync(enrollmentInsertDto);
        }


    [HttpPut("{EnrollmentId}")]
    public async Task<Response<string>> UpdateAsync(int EnrollmentId,EnrollmentUpdateDto enrollment)
    {
        return await enrollmentService.UpdateAsync(EnrollmentId,enrollment);
    }

        [HttpDelete("{enrollmentId}")]
        public async Task<Response<string>> Delete(int enrollmentId)
        {
            return await enrollmentService.DeleteAsync(enrollmentId);
        }

        [HttpGet]
        public async Task<PagedResult<Enrollment>> GetEnrollment([FromQuery] EnrollmentFilter filter, [FromQuery] PagedQuery pagedQuery)
        {
            return await enrollmentService.GetEnrollment(filter, pagedQuery);   
        }

        [HttpGet("{enrollmentId}")]
        public async Task<Response<Enrollment>> GetEnrollmentById(int enrollmentId)
        {
            return await enrollmentService.GetEnrollmentBuyIdAsync(enrollmentId);
        }
    }
}