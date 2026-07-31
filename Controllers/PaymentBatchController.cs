using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using mks.Dtos;
using mks.DTOs;
using mks.Interfaces;

namespace mks.Controllers
{
    [ApiController]
    [Route("api/payment/batch")]

    public class PayamentBatchController : ControllerBase
    {
        private readonly IPaymentBatchService _paymentBatch;

        public PayamentBatchController(IPaymentBatchService payment)
        {
            _paymentBatch=payment;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateBatchPayment(CreatePaymentBatchDto dto)
        {
            var result = await _paymentBatch.CreatePaymentBatchAsync(dto);
            if(!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("get/query")]
        public async Task<IActionResult> FilterBatchPayment([FromQuery]FilterBatchPaymentDto filter)
        {
            var result = await _paymentBatch.FilterBatchAsync(filter);
            if(!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetBatchPayment()
        {
            var result = await _paymentBatch.GetPaymentBatchesAsync();
            if(!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
    }
}