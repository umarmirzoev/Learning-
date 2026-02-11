using System.Net;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;
public interface IUserService
{
    Task<Response<string>> AddUserAsync(UserInsertDto userInsertDto);
    Task<Response<User>> GetUserBuyIdAsync(int userId);
    Task<PagedResult<User>> GetUser(UserFiltres filter, PagedQuery pagedQuery);

    Task<Response<string>> UpdateAsync(int userId,UserUpdateDto userUpdateDto);
    Task<Response<string>> DeleteAsync(int userId);
}