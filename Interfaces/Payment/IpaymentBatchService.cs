using System.ComponentModel.DataAnnotations;
using mks.Dtos;
using mks.DTOs;

namespace mks.Interfaces
{
    public interface IPaymentBatchService
    {
        Task<ServiceResponse>CreatePaymentBatchAsync(CreatePaymentBatchDto dto);
        Task<ServiceResponse>GetPaymentBatchesAsync();
        Task<ServiceResponse>FilterBatchAsync(FilterBatchPaymentDto filter);
    }
}