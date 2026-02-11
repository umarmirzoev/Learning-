using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService ) : ControllerBase
    {
        [HttpPost]
        public async Task<Response<string>> AddAsync(UserInsertDto userInsertDto )
        {
            return await userService. AddUserAsync(userInsertDto);
        }

        [HttpPut("{UserId}")]
    public async Task<Response<string>> UpdateAsync(int UserId,UserUpdateDto User)
    {
        return await userService.UpdateAsync(UserId,User);
    }

        [HttpDelete("{userId}")]
        public async Task<Response<string>> Delete(int userId)
        {
            return await userService.DeleteAsync(userId);
        }

        [HttpGet]
        public async Task<PagedResult<User>> GetUser([FromQuery] UserFiltres filter, [FromQuery] PagedQuery pagedQuery)
        {
            return await userService.GetUser(filter, pagedQuery);   
        }

        [HttpGet("{userId}")]
        public async Task<Response<User>> GetUserById(int userId)
        {
            return await userService.GetUserBuyIdAsync(userId);
        }
    }
}