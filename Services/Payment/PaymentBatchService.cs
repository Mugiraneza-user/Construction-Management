using mks.Data;
using mks.Interfaces;
using mks.DTOs;
using mks.Models;
using Microsoft.EntityFrameworkCore;
using mks.Dtos;
using mks.Enum;
using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore.Query;

namespace mks.Services
{
    public class PaymentBatchService : IPaymentBatchService
    {
        private readonly ApplicationDbContext _context;

        public PaymentBatchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse> CreatePaymentBatchAsync(CreatePaymentBatchDto dto)
        {

            var category = await _context.WorkerCategories.FirstOrDefaultAsync(a=>a.id == dto.category_id);

            if (category == null)
            return new ServiceResponse
            {
                Success=false,
                Message="Category not found"
            };
            var period = await _context.WorkerPeriods.FirstOrDefaultAsync(x => x.id == dto.period_id);

            if (period == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Work period not found"
                };
            }

            var paymentDone= await _context.PaymentBatches.FirstOrDefaultAsync(a=>a.period_id == dto.period_id && a.category_id == dto.category_id);

            if (paymentDone != null)
            return new ServiceResponse
            {
                Success =false,
                Message = "Payment for this batch has already done"
            };
            var payrolls = await _context.payrolls.Include(a => a.worker).Where(a => a.period_id == dto.period_id && a.worker.category_id == dto.category_id).ToListAsync();

                if (!payrolls.Any())
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Message = "No payrolls found for the selected period and category."
                    };
                };
            var alreadyPaid = await _context.payments.Include(a=>a.worker).AnyAsync(a=>a.period_id== dto.period_id && a.worker.category_id == dto.category_id && a.status == PaymentStatus.pending);

            if (alreadyPaid)
            {
                    return new ServiceResponse
                    {
                        Success= false,
                        Message = "All worker has been paid already"
                    } ;   
            }
            

              decimal totalAmount = payrolls.Sum(a=>a.net_salary);


            var batch = new PaymentBatch
            {
                batch_number = $"PB-{DateTime.Now:yyyyMMddHHmmss}",
                period_id = dto.period_id,
                category_id = dto.category_id,
                payment_method = dto.payment_method,
                total_amount =totalAmount,
                payment_date=DateTime.Now,
                status = Enum.PaymentBatchStatus.Completed,
            };
            _context.PaymentBatches.Add(batch);

            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Success = true,
                Message = "Payment batch created successfully",
                Response = batch
            };
        }

        public async Task<ServiceResponse> GetPaymentBatchesAsync()
        {
            var batches = await _context.PaymentBatches.ToListAsync();
            return new ServiceResponse
            {
                Success = true,
                Message = "Payment batches retrieved successfully",
                Response = batches
            };
        }
        public async Task<ServiceResponse> FilterBatchAsync(FilterBatchPaymentDto filter)
        {
            var query = _context.PaymentBatches.AsQueryable();

            if (filter.id.HasValue)
            {
                query = query.Where(a=>a.id == filter.id.Value);
            }
            if (filter.payment_date.HasValue)
            {
                query = query.Where(a=>a.payment_date == filter.payment_date.Value);
            }

            if (filter.paymentMethod.HasValue)
            {
                query=query.Where(a=>a.payment_method == filter.paymentMethod.Value);
            }
            if (filter.category_id.HasValue)
            {
                query=query.Where(a=>a.category_id == filter.category_id.Value);
            }

            var payment = await query.ToListAsync();

            if (!payment.Any())
            {
                return new ServiceResponse
                {
                    Success=false,
                    Message = "No payment found"
                };
            }
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Payment batch not found",
                    Response = payment
                    
                };
            }

        }
    }
}