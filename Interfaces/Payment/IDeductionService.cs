using System.ComponentModel.DataAnnotations;
using mks.Dtos;
using mks.DTOs;

namespace mks.Interfaces
{
    public interface IDeductionService
    {
        Task<ServiceResponse> MakeDeductionAsync(MakeDeductionDto dto);

        Task<ServiceResponse> MarkAsPaidDeductionAsync(MarkAsPaidDeductionDto dto);

        Task<ServiceResponse> DeleteDeductionAsync(DeleteDeduction dto);

        Task<ServiceResponse> UpdateDeductionAsync(UpdateDeductionDto dto);

        Task<ServiceResponse> GetDeductionAsync();
    }
}