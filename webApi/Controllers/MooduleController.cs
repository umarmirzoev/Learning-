using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;
using WebApi.Filters;
using WebApi.Responses;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController(IModuleService moduleService ) : ControllerBase
    {
        [HttpPost]
        public async Task<Response<string>> AddAsync(ModuleInsertDto  moduleInsertDto)
        {
            return await moduleService. AddModuleAsync(moduleInsertDto);
        }


     [HttpPut("{moduleId}")]
    public async Task<Response<string>> UpdateAsync(int moduleId,ModuleUpdateDto model)
    {
        return await moduleService.UpdateAsync(moduleId,model);
    }

        [HttpDelete("{moduleId}")]
        public async Task<Response<string>> Delete(int moduleId)
        {
            return await moduleService.DeleteAsync(moduleId);
        }

        [HttpGet]
        public async Task<PagedResult<Module>> GetModule([FromQuery] ModuleFiltres filter, [FromQuery] PagedQuery pagedQuery)
        {
            return await moduleService.GetModule(filter, pagedQuery);   
        }

        [HttpGet("{moduleId}")]
        public async Task<Response<Module>> GetModelById(int moduleId)
        {
            return await moduleService.GetModuleBuyIdAsync(moduleId);
        }
    }
}