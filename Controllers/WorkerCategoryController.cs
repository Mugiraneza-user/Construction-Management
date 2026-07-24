using Microsoft.AspNetCore.Mvc;

using mks.DTOs;
using mks.Interfaces;
using mks.Services;

namespace mks.Controllers
{
    [ApiController]
    [Route("api/admin/category")]
    public class CategoryController : ControllerBase
    {
        private readonly  IWorkerCategoryService  _WorkerCategory;

        public CategoryController(IWorkerCategoryService workerCategory)
        {
            _WorkerCategory=workerCategory;
        }


       [HttpPost("create")]
        public async Task<IActionResult> AddCategory(AddWorkerCategoryDto dto)
        {
            var result= await _WorkerCategory.AddWorkerCategoryAsync(dto);
            if (!result.Success)
                return BadRequest(result);
             return Ok(result);   
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCategory(DeleteWorkerCategoryDto dto)
        {
            var result = await _WorkerCategory.DeleteWorkerCategoryAsync(dto);
            if(!result.Success)
              return BadRequest(result);
            return Ok(result);  
        }
        [HttpPatch("Activate")]
        public async Task<IActionResult> ActivateCategory(ActivateWorkerCategoryDto dto)
        {
            var result = await _WorkerCategory.ActivateWorkerCategoryAsync(dto);
            if (!result.Success)

               return BadRequest(result);

            return Ok(result);
        }
        [HttpPatch("Deactivate")]
        public async Task<IActionResult> DeactivateCategory(DeactivateWorkerCategoryDto dto)
        {
            var result = await _WorkerCategory.DeactivateWorkerCategoryAsync(dto);

            if(!result.Success)

            return BadRequest(result);

             return Ok (result);
        }
        [HttpPatch("update")]
        public async Task<IActionResult> UpdateCategory(UpdateWorkerCategoryDto dto)
        {
            var result= await _WorkerCategory.UpdateWorkerCategoryAsync(dto);
            if (!result.Success)
               return BadRequest(result);

            return Ok(result);   

        }

        [HttpGet("all")]
        
        public async Task<IActionResult> GetCategory()
        {
            var result= await _WorkerCategory.ListAllWorkerCategoryAsync();

            if (!result.Success)
            return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("all/query")]
        public async Task<IActionResult> GetCategory([FromQuery] WorkerCategoryFilterDto filter)
        {
            var result = await _WorkerCategory.WorkerCategoryFilterAsync(filter);

            if(!result.Success)
            return BadRequest(result);

            return Ok (result);
        }
        
    }
}