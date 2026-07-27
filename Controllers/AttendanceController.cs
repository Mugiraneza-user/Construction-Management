using Microsoft.AspNetCore.Mvc;
using mks.Dtos;
using mks.Services;
using mks.Interfaces;

namespace mks.Controllers
{
    [ApiController]
    [Route("api/attendance")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendance;

        public AttendanceController(IAttendanceService attendance)
        {
            _attendance = attendance;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAttendance([FromBody] CreateAttendanceDto dto)
        {
            var result = await _attendance.CreateAttendanceAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAttendance([FromBody] UpdateAttendanceDto dto)
        {
            var result = await _attendance.UpdateAttendanceAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAttendance(DeleteAttendanceDto dto)
        {
            var result = await _attendance.DeleteAttendanceAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("query")]
        public async Task<IActionResult> FilterAttendance([FromQuery] FilterAttendanceDto filter)
        {
            var result = await _attendance.FilterAttendanceAsync(filter);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAttendance()
        {
            var result = await _attendance.GetAttendanceAsync();

            if(!result.Success)
            return BadRequest(result);
            return Ok(result);
        }
        
        [HttpGet("get/id")]
        public async Task<IActionResult> GetAttendanceById([FromQuery]GetAttendanceByIdDto filter)
        {
            var result = await _attendance.GetAttendanceByIdAsync(filter);

            if(!result.Success)
            return BadRequest(result);
            return Ok(result);
        }
    }
}