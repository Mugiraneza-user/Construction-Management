using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using mks.DTOs;
using mks.Services;
using mks.Interfaces;

namespace mks.Controllers
{
    [ApiController]
    [Route("api/worker")]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _worker;

        public WorkerController(IWorkerService worker)
        {
            _worker= worker;
        }

       [HttpPost("create")]
       public async Task<IActionResult> CreateWorkerAsync(CreateWorkerDto dto)
        {
            var result = await _worker.CreateWorkerAsync(dto);

            if (!result.Success)

            return BadRequest(result);

            return Ok(result);
        }
        [HttpPatch("update")]
        public async Task<IActionResult> UpdateWorkerAsync(UpdateWorkerDto dto)
        {
            var result = await _worker.UpdateWorkerAsync(dto);

            if(!result.Success)

            return BadRequest(result);
            return Ok (result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteWorkerAsync(DeleteWorkerDto dto)
        {
            var result  = await _worker.DeleteWorkerAsync(dto);

            if(!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("get")]
        public async Task <IActionResult> GetWorkerAsync()
        {
            var result = await _worker.ListAllWorkerAsync();
            if(!result.Success)
            return BadRequest(result);

            return Ok(result);
    
        }
        [HttpGet("query")]
        public async Task<IActionResult> FilterWorkerAsync(FilterWorkerDto filter)
        {
            var result = await _worker.FilterWorkerAsync(filter);

            if(!result.Success)

            return BadRequest(result);

            return Ok(result);
        }
    }


}