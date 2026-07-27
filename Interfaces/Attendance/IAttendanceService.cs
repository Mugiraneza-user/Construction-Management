using System.ComponentModel.DataAnnotations;
using mks.Dtos;
using mks.DTOs;

namespace mks.Interfaces
{
    public interface IAttendanceService
    {
        Task<ServiceResponse> CreateAttendanceAsync(CreateAttendanceDto dto);

        Task<ServiceResponse> UpdateAttendanceAsync(UpdateAttendanceDto dto);

        Task<ServiceResponse> DeleteAttendanceAsync(DeleteAttendanceDto dto);
        Task<ServiceResponse> GetAttendanceAsync();

        Task<ServiceResponse> FilterAttendanceAsync(FilterAttendanceDto dto);

        Task <ServiceResponse> GetAttendanceByIdAsync(GetAttendanceByIdDto filter);
    }
}