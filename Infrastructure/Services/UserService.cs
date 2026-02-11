using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Services;

public class UserService : IUserService
{
    private readonly ApplicationDBContext _dbContext;
    private readonly ILogger<UserService> _logger;

    public UserService(ApplicationDBContext dbContext, ILogger<UserService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Response<string>> AddUserAsync(UserInsertDto dto)
    {
        try
        {
            var user = new User
            {
                Fullname = dto.Fullname,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return new Response<string>(HttpStatusCode.OK, "User added successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Add user failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<string>> DeleteAsync(int userId)
    {
        try
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null) return new Response<string>(HttpStatusCode.NotFound, "User not found");

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete user failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<User>> GetByIdAsync(int userId)
    {
        try
        {
            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return new Response<User>(HttpStatusCode.NotFound, "User not found");

            return new Response<User>(HttpStatusCode.OK, "Success", user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get user by id failed");
            return new Response<User>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public Task<PagedResult<User>> GetUser(UserFiltres filter, PagedQuery pagedQuery)
    {
        throw new NotImplementedException();
    }

    public Task<Response<User>> GetUserBuyIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResult<User>> GetUsers(UserFiltres filter, PagedQuery pagedQuery)
    {
        var page = pagedQuery.Page <= 0 ? 1 : pagedQuery.Page;
        var pageSize = pagedQuery.PageSize <= 0 ? 10 : pagedQuery.PageSize;

        IQueryable<User> query = _dbContext.Users.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter?.Fullname))
            query = query.Where(x => x.Fullname!.Contains(filter.Fullname));

        var totalCount = await query.CountAsync();
        var items = await query.OrderBy(x => x.Id) .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<User>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public async Task<Response<string>> UpdateAsync(int userId, UserUpdateDto userUpdateDto)
    {
        try
        {
            var user1 = await _dbContext.Users.FindAsync(userUpdateDto);
            if (user1 == null) return new Response<string>(HttpStatusCode.NotFound, "User not found");

            user1.Fullname = userUpdateDto.Fullname;
            user1.Email = userUpdateDto.Email;

            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update user failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

}
