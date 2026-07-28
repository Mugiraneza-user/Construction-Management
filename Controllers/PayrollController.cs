using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using mks.DTOs;
using mks.Interfaces;

namespace mks.Controllers
{
    [ApiController]
    [Route("api/payroll")]
    public class PayrollController : ControllerBase

    {
        private readonly IPayRollService _payRoll;
        public PayrollController(IPayRollService payRoll)
        {
            _payRoll = payRoll;
        }
     [HttpPost("create")]
     public async Task <IActionResult> CreatePayroll(CreatePayrollDto dto)
        {
            var result = await _payRoll.CreatePayrollAsync(dto);

            if(!result.Success)
            return BadRequest(result);
            return Ok(result);
        }
        [HttpPatch("update")]
         public async Task <IActionResult> UpdatePayroll(UpdatePayrollDto dto)
        {
            var result = await _payRoll.UpdatePayrollAsync(dto);

            if(!result.Success)
            return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("get/query")]
         public async Task <IActionResult> FilterPayroll([FromQuery]PayrollFilterDto filter)
        {
            var result = await _payRoll.FilterPayrollAsync(filter);

            if(!result.Success)
            return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("get")]
         public async Task <IActionResult> GetAllPayroll()
        {
            var result = await _payRoll.GetPayrollAsync();

            if(!result.Success)
            return BadRequest(result);
            return Ok(result);
        }

    }
}