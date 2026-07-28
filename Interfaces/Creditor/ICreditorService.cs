using System.ComponentModel.DataAnnotations;
using mks.DTOs;

namespace mks.Interfaces
{
    public interface ICreditorService
    {
        Task<ServiceResponse> CreateCreditorAsync(CreateCreditorDto dto);

        Task<ServiceResponse> DeleteCreditorAsync(DeleteCreditorDto dto);

        Task <ServiceResponse> UpdateCreditorAsync(UpdateCreditorDto dto);

        Task<ServiceResponse> GetAllCreditorAsync();
    }
}