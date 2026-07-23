using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using mks.DTOs;
using mks.Interfaces;

namespace mks.Controllers
{
    [ApiController]
    [Route("api/admin/period")]
    public class WorkerPeriodController  : ControllerBase
    {
        private readonly IWorkerPeriodService _workerPeriod;

        public WorkerPeriodController(IWorkerPeriodService workPeriod)
        {
            _workerPeriod = workPeriod ;
        }

        [HttpPost ("Create")]

    public async Task<IActionResult> CreatePeriodWorker(CreateWorkerPeriodDto dto)
        {
            var result = await _workerPeriod.CreateWorkerPeriodAsync(dto);
            if(!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
        [HttpPatch("update")]
         public async Task<IActionResult> UpdatePeriodWorker(UpdateWorkerPeriodDto dto)
        {
            var result = await _workerPeriod.UpdateWorkerPeriodAsync(dto);
            if(!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
         [HttpPatch("update/status")]
         public async Task<IActionResult> UpdateWorkerPeriodStatus(UpdateWorkerPeriodStatusDtoo dto)
        {
            var result = await _workerPeriod.UpdateWorkerPeriodStatusAsync(dto);
            if(!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
         [HttpGet("period/query")]
         public async Task<IActionResult> FilterWorkerPeriod([FromQuery]FilterWorkerPeriodDto filter)
        {
            var result = await _workerPeriod.FilterWorkerPeriodAsync(filter);
            if(!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
          [HttpGet("period")]
         public async Task<IActionResult> GetWorkerPeriod()
        {
            var result = await _workerPeriod.GetWorkerPeriodAsync();
            if(!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
    }
}