using System.ComponentModel.DataAnnotations;
using mks.DTOs;
using mks.Models;

namespace mks.Interfaces
{
    public interface IPayRollService
    {
        Task<ServiceResponse> CreatePayrollAsync(CreatePayrollDto dto);

       Task<ServiceResponse> UpdatePayrollAsync(UpdatePayrollDto dto);

        Task<ServiceResponse> DeletePayrollAsync(int id);

        Task<ServiceResponse> FilterPayrollAsync(PayrollFilterDto filter);

         Task<ServiceResponse> GetPayrollAsync();
    }
}


