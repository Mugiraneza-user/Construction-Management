using System.ComponentModel.DataAnnotations;
using mks.Interfaces;
using mks.Services;
using mks.Dtos;
using Microsoft.AspNetCore.Mvc;
using mks.DTOs;

namespace mks.Controllers
{
    [ApiController]
    [Route("api/creditor")]

    public class CreditorController : ControllerBase
    {
        private readonly ICreditorService _creditor;

        public CreditorController(ICreditorService creditor)
        {
            _creditor = creditor;
        }

        [HttpPost("create")]
        public async Task <IActionResult> CreateCreditor(CreateCreditorDto dto)
        {
            var result = await _creditor.CreateCreditorAsync(dto);

            if (!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
         [HttpDelete("delete")]
        public async Task <IActionResult> DeleteCreditor(DeleteCreditorDto dto)
        {
            var result = await _creditor.DeleteCreditorAsync(dto);

            if (!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
         [HttpPatch("update")]
        public async Task <IActionResult> UpdateCreditor(UpdateCreditorDto dto)
        {
            var result = await _creditor.UpdateCreditorAsync(dto);

            if (!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
         [HttpGet("get")]
        public async Task <IActionResult> GetAllCreditor()
        {
            var result = await _creditor.GetAllCreditorAsync();

            if (!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
    }
}