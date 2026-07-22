using Microsoft.AspNetCore.Mvc;
using mks.DTOs;
using mks.Dtos;
using mks.Interfaces;
using mks.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace mks.Controllers
{
    [ApiController]
    [Route("api/admin/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IUpdateRoleService _updateRoles;

        public RolesController(IUpdateRoleService updateRoles)
        {
            _updateRoles = updateRoles;
        }


        [HttpPost("update")]
        public async Task<IActionResult> Update(UpdateRolesDto dto)
        {
            var result= await _updateRoles.UpdateRoleAsync(dto);

            if (!result.Success)
        
                return BadRequest (result);

            return Ok(result);  
            
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateRoleDto dto)
        {
            var result = await _updateRoles.CreateRoleAsync(dto);
            if(!result.Success)
                return BadRequest(result);

            return Ok(result);  
            
        }

        [HttpPatch("activate")]

        public async Task<IActionResult> Activate(ActivateRoleDto dto)
        {
            var result = await _updateRoles.ActivateRoleAysnc(dto);
            if(!result.Success)
            return BadRequest(result);

            return Ok(result);
        }
        [HttpPatch("Deactivate")]

        public async Task<IActionResult> Deactivate(DeactivateRoleDto dto)
        {
            var result = await _updateRoles.DeactivateRoleAysnc(dto);

            if(!result.Success)
               return BadRequest (result);

            return Ok(result);   
        }
        [HttpGet("Get")]
        public async Task<IActionResult> GetallRoles()
        {
            var result = await _updateRoles.GetAllRoleAsync();

            if(!result.Success)

            return BadRequest(result);

            return Ok (result);
        }
        [HttpGet("Get/query")]
        public async Task <IActionResult> GetRoleFilter([FromQuery]RoleFilterDto filter)
        {
            var result= await _updateRoles.RoleFilterAsync(filter);

            if(!result.Success)
              return BadRequest(result);

              return Ok(result);
        }
    }

}