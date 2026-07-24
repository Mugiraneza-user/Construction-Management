using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using mks.Data;
using mks.DTOs;
using mks.Interfaces;
using mks.model;

namespace mks.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly ApplicationDbContext _context;

        public WorkerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse> CreateWorkerAsync(CreateWorkerDto dto)
        {
            var worker = await _context.workers.AnyAsync(a=>a.worker_number == dto.full_name);

            if (worker)
            return new ServiceResponse
            {
                Success= false,
                Message= "Worker Allready Exist"
            };

            var addWorker = new Worker
            {
                full_name= dto.full_name,
                national_id=dto.national_id,
                status=dto.status,
                shift=dto.shift,
                telephone=dto.telephone,
                category_id=dto.category_id,
                bank_account=dto.bank_account,

            };

            _context.workers.Add(addWorker);

            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Success= true,
                Message = "Worker has been added ssuccessfuly",
                Response= addWorker
            };
        }

        public async Task<ServiceResponse> UpdateWorkerAsync(UpdateWorkerDto dto)
        {
            var worker = await _context.workers.FirstOrDefaultAsync(a=>a.worker_number == dto.worker_number);

            if(worker == null)
            return new ServiceResponse
            {
                Success= false,
                Message= "Worker not found"
            };
            
           worker.full_name= dto.full_name;
           worker.national_id=dto.national_id;
           worker.bank_account=dto.bank_account;
           worker.telephone= dto.telephone;
           worker.status=dto.status;
           worker.shift= dto.shift;
           worker.category_id=dto.category_id;

           await _context.SaveChangesAsync();

           return new ServiceResponse
           {
               Success= true,
               Message= "Worker information updated successfully",
               Response= worker
           };


        }
        public async Task <ServiceResponse> DeleteWorkerAsync(DeleteWorkerDto dto)
        {
            var worker = await _context.workers.FirstOrDefaultAsync(a=>a.worker_number == dto.worker_number);

            if (worker== null)
            return new ServiceResponse
            {
                Success= false,
                Message= "Worker not found"
            };

            _context.workers.Remove(worker);

            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Success=true,
                Message = "Worker deleted successfully"
            };
        }
        public async Task <ServiceResponse> FilterWorkerAsync(FilterWorkerDto filter)
        {
            var query= _context.workers.AsQueryable();

            if (string.IsNullOrEmpty(filter.full_name))
            {
                query= query.Where(a=>a.full_name.Contains(filter.full_name!));
            }
            if (filter.category_id.HasValue)
            {
                query= query.Where(a=>a.category_id == filter.category_id);
            }

            if (filter.date_joined.HasValue)
            {
                query= query.Where(a=>a.date_joined == filter.date_joined);
            }
            if (filter.national_id.HasValue)
            {
                query = query.Where(a=>a.national_id == filter.national_id);
            }

            if (string.IsNullOrEmpty(filter.bank_account))
            {
                query = query.Where(a=>a.bank_account.Contains(filter.bank_account!));
            }
            if (string.IsNullOrEmpty(filter.worker_number))
            {
                query = query.Where(a=>a.worker_number.Contains(filter.worker_number!));
            }
            if (filter.telephone.HasValue)
            {
                query=query.Where(a=>a.telephone == filter.telephone);
            }
            if(!query.Any())
            return new ServiceResponse
            {
                Success=false,
                Message="Worker not found"
            };
            return new ServiceResponse
            {
                Success=true,
                Message="Worker retrived successsfully ",
                Response = query
            };

        }
        public async Task<ServiceResponse> ListAllWorkerAsync()
        {
            var worker = await _context.workers.ToListAsync();

            return new ServiceResponse
            {
                Success= true,
                Message= "Worker retrived successfully",
                Response= worker
            };
        }
    }
}