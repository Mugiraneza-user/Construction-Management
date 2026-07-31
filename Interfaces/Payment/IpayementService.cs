using System.ComponentModel.DataAnnotations;
using mks.DTOs;
using mks.Dtos;

namespace mks.Interfaces
{
    public interface IPaymentService
    {
        Task<ServiceResponse> CreatePaymentAsync(CreatePersonPaymentDto dto);

        Task<ServiceResponse>FilterPaymentAsync(FilterPaymentDto filter); 

        Task<ServiceResponse> GetPaymentsAsync();
    }
}