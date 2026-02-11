using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Services;

public class ModuleService : IModuleService
{
    private readonly ApplicationDBContext _dbContext;
    private readonly ILogger<ModuleService> _logger;

    public ModuleService(ApplicationDBContext dbContext, ILogger<ModuleService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Response<string>> AddModuleAsync(ModuleInsertDto dto)
    {
        try
        {
            var module = new Module
            {
                Title = dto.Title,
                CourseId = dto.CourseId,
                Order = dto.Order
            };

            _dbContext.Modules.Add(module);
            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Module added successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Add Module failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        try
        {
            var module = await _dbContext.Modules.FindAsync(id);
            if (module == null) return new Response<string>(HttpStatusCode.NotFound, "Module not found");

            _dbContext.Modules.Remove(module);
            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete Module failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<Response<Module>> GetModuleBuyIdAsync(int id)
    {
        try
        {
            var module = await _dbContext.Modules.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (module == null) return new Response<Module>(HttpStatusCode.NotFound, "Module not found");

            return new Response<Module>(HttpStatusCode.OK, "Success", module);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get Module by id failed");
            return new Response<Module>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }

    public async Task<PagedResult<Module>> GetModule(ModuleFiltres filter, PagedQuery pagedQuery)
    {
        var page = pagedQuery.Page <= 0 ? 1 : pagedQuery.Page;
        var pageSize = pagedQuery.PageSize <= 0 ? 10 : pagedQuery.PageSize;

        IQueryable<Module> query = _dbContext.Modules.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter?.Title))
            query = query.Where(x => x.Title!.Contains(filter.Title));

        var totalCount = await query.CountAsync();
        var items = await query.OrderBy(x => x.Order).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResult<Module>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public async Task<Response<string>> UpdateAsync(int id, ModuleUpdateDto dto)
    {
        try
        {
            var module = await _dbContext.Modules.FindAsync(id);
            if (module == null) return new Response<string>(HttpStatusCode.NotFound, "Module not found");

            module.Title = dto.Title;
            module.Order = dto.Order;

            await _dbContext.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Update Module failed");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong");
        }
    }




}
