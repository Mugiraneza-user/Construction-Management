using mks.DTOs;
using mks.Interfaces;
using mks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using mks.Dtos;
using System.Net;
using System.Net.Mime;
using mks.Data;

namespace mks.Services
{
    public class UpdateRoleService : IUpdateRoleService

{
       private readonly ApplicationDbContext _context;

        public UpdateRoleService(ApplicationDbContext context)
        {
            _context = context;
        }
    public async Task<ServiceResponse> UpdateRoleAsync(UpdateRolesDto dto)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.id);

            if (user == null)
            return new ServiceResponse
            {
                Success=false ,
                Message= "User id is not found ",

            };
              var role = await _context.Role.FirstOrDefaultAsync(a=> a.id == dto.role_id);

             if (role == null)
             return new ServiceResponse
            {
                Success=false ,
                Message= "Role id is not found " ,

            };
              user.role_id = dto.role_id;

                await _context.SaveChangesAsync();
                 return new ServiceResponse
                 {
                Success = true,
                Message = "User role updated successfully.",
                  };

             
    }

    public async Task<ServiceResponse> CreateRoleAsync(CreateRoleDto dto)
        {
            var roleNameExist= await _context.Role.AnyAsync(u=>u.role_name == dto.role_name && u.description == dto.description );


            if (roleNameExist)
            return new ServiceResponse
            {
                Success=false,
                Message="Role name already exist",
            };
            var role= new Role
            {
            role_name = dto.role_name,
            description = dto.description,
            is_active = false
            };

            _context.Role.Add(role);

            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                Success= true,
                Message = "Roles created successfully",
            };



        }
        public async Task<ServiceResponse> ActivateRoleAysnc(ActivateRoleDto dto)
        {
            var role= await _context.Role.FirstOrDefaultAsync(a=>a.id == dto.role_id);

            if (role == null)
{
                    return new ServiceResponse
                    {
                        Success = false,
                        Message = "Role not found."
                    };
                }

            if (role.is_active == true)
            return new ServiceResponse
            { 
                Success=false,
                Message="Role is already active",
                
            };
           role.is_active = true;

           await _context.SaveChangesAsync();

           return new ServiceResponse
           {
               Success= true,
               Message= "Role is activated successfully",
           };
            
        }
        public async Task<ServiceResponse> DeactivateRoleAysnc(DeactivateRoleDto dto)
        {
            var role= await _context.Role.FirstOrDefaultAsync(a=>a.id == dto.role_id);

            if (role == null)
{
                    return new ServiceResponse
                    {
                        Success = false,
                        Message = "Role not found."
                    };
                }

            if (role.is_active == false)
            return new ServiceResponse
            {
                Success=false,
                Message="Role is Deactivated already"
            };

            role.is_active = false;
            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                Success=true,
                Message= "Role is deactivated successfully"
            };
        }

        public async Task<ServiceResponse> GetAllRoleAsync()
        {
            var role= await _context.Role.ToListAsync();

            return new ServiceResponse
            {
                Success= true,
                Message= "Roles retrived well",
                Response= role
            };
        }
}
}

