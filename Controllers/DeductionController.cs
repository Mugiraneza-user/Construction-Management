using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using mks.Dtos;
using mks.DTOs;
using mks.Interfaces;
using mks.Services;

namespace mks.Controllers
{
 [ApiController]
 [Route ("api/deduction")]

 public class DeductionController : ControllerBase
    {
        private readonly IDeductionService _deduction;

        public DeductionController (IDeductionService deduction)
        {
            _deduction = deduction;
        }
         [HttpPost("make")]
        public async Task <IActionResult> MakeDeduction(MakeDeductionDto dto)
        {
            var result = await _deduction.MakeDeductionAsync(dto);

            if(!result.Success)

            return BadRequest(result);

            return Ok(result);
        }
        [HttpPost("paid")]

        public async Task<IActionResult> MakeAsPaid(MarkAsPaidDeductionDto dto)
        {
            var result = await _deduction.MarkAsPaidDeductionAsync(dto);

            if(!result.Success)

            return BadRequest(result);

            return Ok(result);
        }
        [HttpDelete("delete")]

        public async Task<IActionResult> DeleteDeduction(DeleteDeduction dto)
        {
            var result = await _deduction.DeleteDeductionAsync(dto);

            if(!result.Success)
            return BadRequest(result);
            return Ok(result);
        }
        [HttpPatch("update")]

        public async Task<IActionResult> UpdateDeduction(UpdateDeductionDto dto)
        {
            var result = await _deduction.UpdateDeductionAsync(dto);

            if(!result.Success)

            return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("get")]

        public async Task<IActionResult> GetDeduction()
        {
            var result = await _deduction.GetDeductionAsync();

            if(!result.Success)

            return BadRequest(result);

            return Ok(result);
        }
    }
}