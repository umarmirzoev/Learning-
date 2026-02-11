using System.Net;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;
public interface IModuleService
{
    Task<Response<string>> AddModuleAsync(ModuleInsertDto moduleInsertDto);
    Task<Response<Module>> GetModuleBuyIdAsync(int moduleId);
    Task<PagedResult<Module>> GetModule(ModuleFiltres filter, PagedQuery pagedQuery);

    Task<Response<string>> UpdateAsync(int moduleId, ModuleUpdateDto moduleUpdateDto);
    Task<Response<string>> DeleteAsync(int moduleId);
}