using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using mks.Dtos;
using mks.Interfaces;

namespace mks.Controllers
{
    [ApiController]
    [Route("api/payment")]

    public class PayamentController : ControllerBase
    {
        private readonly IPaymentService _payment;

        public PayamentController(IPaymentService payment)
        {
            _payment=payment;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment(CreatePersonPaymentDto dto)
        {
            var result = await _payment.CreatePaymentAsync(dto);
            if(!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("get/query")]
        public async Task<IActionResult> FilterPayment([FromQuery]FilterPaymentDto filter)
        {
            var result = await _payment.FilterPaymentAsync(filter);
            if(!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetPayment()
        {
            var result = await _payment.GetPaymentsAsync();
            if(!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
    }
}