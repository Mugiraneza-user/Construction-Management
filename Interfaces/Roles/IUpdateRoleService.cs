using  mks.DTOs;
using mks.Dtos;

namespace mks.Interfaces

{
    public interface IUpdateRoleService
    {
        Task<ServiceResponse> UpdateRoleAsync(UpdateRolesDto dto);

         Task<ServiceResponse> CreateRoleAsync(CreateRoleDto dto);

         Task<ServiceResponse> ActivateRoleAysnc(ActivateRoleDto dto);

         Task<ServiceResponse>  DeactivateRoleAysnc(DeactivateRoleDto dto);
    }
}