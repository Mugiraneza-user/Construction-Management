using System.ComponentModel.DataAnnotations;
using mks.DTOs;

namespace mks.Interfaces
{
    public interface IWorkerService
    {
        Task<ServiceResponse> CreateWorkerAsync(CreateWorkerDto dto);
        Task<ServiceResponse> UpdateWorkerAsync( UpdateWorkerDto dto);

        Task<ServiceResponse> DeleteWorkerAsync(DeleteWorkerDto dto);


        Task<ServiceResponse> FilterWorkerAsync(FilterWorkerDto filter);

        Task <ServiceResponse> ListAllWorkerAsync();
    }
}