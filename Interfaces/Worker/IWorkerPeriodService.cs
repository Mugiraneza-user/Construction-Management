using System.ComponentModel.DataAnnotations;
using mks.DTOs;


namespace mks.Interfaces
{
    public interface IWorkerPeriodService
    {
        Task<ServiceResponse> CreateWorkerPeriodAsync(CreateWorkerPeriodDto dto);

        Task <ServiceResponse> UpdateWorkerPeriodAsync(UpdateWorkerPeriodDto  dto);
        Task<ServiceResponse> UpdateWorkerPeriodStatusAsync(UpdateWorkerPeriodStatusDtoo dto);

        Task <ServiceResponse> FilterWorkerPeriodAsync(FilterWorkerPeriodDto filter);

        Task<ServiceResponse> GetWorkerPeriodAsync();
    }
}