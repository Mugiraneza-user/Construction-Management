using System.Buffers;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using mks.Data;
using mks.DTOs;
using mks.Interfaces;

using mks.Models;

namespace mks.Services
{
    public class WorkerPeriodService : IWorkerPeriodService
    {
        private readonly ApplicationDbContext _context;

        public WorkerPeriodService (ApplicationDbContext context)
        {
            _context= context;
        }

        public async Task<ServiceResponse> CreateWorkerPeriodAsync(CreateWorkerPeriodDto dto)
        {
            var period = await _context.WorkerPeriods.AnyAsync(a=> a.id == dto.id);

            if(period)
            return new ServiceResponse
            {
                Success= false,
                Message="Id already used"
            };

            var periodDate = await _context.WorkerPeriods.FirstOrDefaultAsync(a=> a.start_date == dto.start_date || a.end_date == dto.end_date);

            if(periodDate != null)
            return new ServiceResponse
            {
                Success= false,
                Message= "Period already used , create a new one "
            };
            var addPeriod= new WorkerPeriod
            {
              name= dto.name,
              start_date= dto.start_date,
              end_date= dto.end_date,  
            };
            _context.WorkerPeriods.Add(addPeriod);

            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Success= true,
                Message= "Worker Period added successfully"
            };
        }
        public async Task<ServiceResponse> UpdateWorkerPeriodAsync(UpdateWorkerPeriodDto dto)
        {
            var period = await _context.WorkerPeriods.FirstOrDefaultAsync(a=>a.id == dto.id);

            if(period == null)
            return new ServiceResponse
            {
              Success = false,
              Message = "Id not found"  
            };

            
                period.name= dto.name;
                period.start_date = dto.start_date;
                period.end_date= dto.end_date;
                period.status = dto.status;
            

            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Success= true,
                Message = "Worker Period Updated successfully "
            };
        }
        public async Task<ServiceResponse> UpdateWorkerPeriodStatusAsync(UpdateWorkerPeriodStatusDtoo dto)
        {
            var status = await _context.WorkerPeriods.FirstOrDefaultAsync(a=>a.id == dto.id);

            if( status== null)
            return new ServiceResponse
            {
                Success= false,
                Message = "PeriodId not found"
            };

            status.status = dto.status;
            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                Success= true,
                Message= "Status Updated successfully",
            };
        }
        public async Task<ServiceResponse> FilterWorkerPeriodAsync(FilterWorkerPeriodDto filter)
        {
            var query =  _context.WorkerPeriods.AsQueryable();

            if (filter.id.HasValue)
            {
                query = query.Where(a=>a.id == filter.id.Value);
            }
            if (!string.IsNullOrEmpty(filter.name))
            {
                query= query.Where(a=> a.name.Contains(filter.name));
            }
            if (filter.status.HasValue)
            {
                query= query.Where(a=>a.status == filter.status);
            }

            if (filter.start_date.HasValue)
            {
                query = query.Where(a=>a.start_date == filter.start_date);
            }
            if (filter.end_date.HasValue)
            {
                query= query.Where(a=>a.end_date == filter.end_date);
            }

           var workPeriod = await query.ToListAsync();
           if(!workPeriod.Any())
           return new ServiceResponse
           {
               Success= false,
               Message= "Woker period not found"
           };

           return new ServiceResponse
           {
               Success= true,
               Message= "Worker period filtered successfully",
               Response=workPeriod 
           }; 
            
        }
        public async Task<ServiceResponse> GetWorkerPeriodAsync()
        {
            var period = await _context.WorkerPeriods.ToListAsync();

            return new ServiceResponse
            {
                Success= true,
                Message= "Worker Period Successfull",
                Response = period
            };
        }
    }
}