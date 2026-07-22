using System.ComponentModel.DataAnnotations;

using mks.DTOs;
using mks.Enum;
using mks.Models;
using mks.Interfaces;
using mks.Data;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System.Buffers;
using System.Text.Json;


namespace mks.Services
{
    public class WorkerCategoryService : IWorkerCategoryService
    {
        private readonly ApplicationDbContext _context; 

        public  WorkerCategoryService(ApplicationDbContext context)
        {
            _context =  context;
        }

        public async Task<ServiceResponse> AddWorkerCategoryAsync(AddWorkerCategoryDto dto )
        {
            
            var category= await _context.WorkerCategories.AnyAsync(a=> a.name==dto.name);
             
            if (category)
             return new ServiceResponse
            {
                 Success = false,
                 Message="Category already exist",
            };
            var addCategory = new WorkerCategory
            {
              name = dto.name,
              salary_per_day= dto.salary_per_day,
              hours_per_day = dto.hours_per_day  
            };
            _context.WorkerCategories.Add(addCategory);
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Success = true,
                 Message= "Worker category has been added successfully",

            };
        }
        public async Task <ServiceResponse> DeleteWorkerCategoryAsync(DeleteWorkerCategoryDto dto)
        {
            
            var category = await _context.WorkerCategories.FirstOrDefaultAsync(a=>a.id == dto.id);

            if(category == null)
            return new ServiceResponse
            {
                Success = false,
                Message = "Worker cayegory  is not found",

            };
             _context.WorkerCategories.Remove(category);
             await _context.SaveChangesAsync();
             return new ServiceResponse
             {
                 Success = true,
                 Message = $"Worker `{category.name}` category is deleted successfulyy"
             };


        }

        public async Task<ServiceResponse> RenameWorkerCategoryAsync(RenameWorkerCategoryDto dto)
        {
            var category = await _context.WorkerCategories.FirstOrDefaultAsync(a=>a.id== dto.id);

            if(category== null)
            return new ServiceResponse
            {
                Success= false,
                Message="Worker category not found"            
          };
            
             
             category.name=dto.name;
            
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Success=true,
                Message= $"Worker {category.name} category has been renamed well"
            };
           
        }

       public async Task<ServiceResponse> ActivateWorkerCategoryAsync(ActivateWorkerCategoryDto dto)
        {
            var category= await _context.WorkerCategories.FirstOrDefaultAsync(a=>a.id==dto.id );

            if (category == null)
            return new ServiceResponse
            {
              Success= false,
              Message= "Category worker  not found"  
            };
           if(category.is_active == true)
            return new ServiceResponse
            {
                Success= false,
                Message= "Category work is active"
            };
            category.is_active = true;
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
              Success= true,
              Message= "Worker category has been activated successsfully"  
            };

        }
        public async Task<ServiceResponse> DeactivateWorkerCategoryAsync(DeactivateWorkerCategoryDto dto)
        {
            var category = await _context.WorkerCategories.FirstOrDefaultAsync(a=>a.id==dto.id);

            if (category== null)
            return new ServiceResponse
            {
                Success= false,
                Message= "Category worker not found"
            };

            if(category.is_active == false)
            return new ServiceResponse
            {
                Success=false,
                Message= "Category worker is already deactivated"
            };
            category.is_active=false;
            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Success= true,
                Message= "The category worker is deactivated successfully"
            };
        }
        public async Task<ServiceResponse> ListAllWorkerCategoryAsync()
        {
            var category = await _context.WorkerCategories.ToListAsync();

            return new ServiceResponse
            {
                Success= true,
                Message= "Worker Categories Retrieved successfully",
                Response = category,
            };
        }
    }
}