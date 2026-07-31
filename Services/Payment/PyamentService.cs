using mks.Data;
using mks.Interfaces;
using mks.DTOs;
using mks.Models;
using Microsoft.EntityFrameworkCore;
using mks.Dtos;

namespace mks.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse> CreatePaymentAsync(CreatePersonPaymentDto dto)
        {  
            var worker = await _context.workers.FirstOrDefaultAsync(a => a.id == dto.worker_id);

            if(worker == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Worker not found"
                };
            }

             var payroll = await _context.payrolls.FirstOrDefaultAsync(a => a.id == dto.payroll_id && a.period_id == dto.period_id );
            if(payroll == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Payroll not found"
                };
            }
            var payment = new Payment
            {
                payment_number = $"PAY-{DateTime.Now:yyyyMMddHHmmss}",

                payroll_id = dto.payroll_id,
                period_id=dto.period_id,
                worker_id = dto.worker_id,

                amount = payroll.net_salary,
                notes = dto.notes,

                status = Enum.PaymentStatus.paid
            };
            _context.payments.Add(payment);
            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                Success = true,
                Message = "Payment created successfully",
                Response = payment
            };

        }
       
        public async Task<ServiceResponse> GetPaymentsAsync()
        {

            var payments = await _context.payments .ToListAsync();

            return new ServiceResponse
            {
                Success = true,
                Message =  "Payments retrieved successfully",
                Response = payments
            };

        }

        public async Task<ServiceResponse> FilterPaymentAsync(FilterPaymentDto filter)
        {
            var query = _context.payments.AsQueryable();

            if (filter.id.HasValue)
            {
                query = query.Where(a=>a.id == filter.id.Value);
            }

            if (filter.payment_date.HasValue)
            {
                query= query.Where(a=>a.Payment_date == filter.payment_date.Value);
            }

            if (filter.paymentMethod.HasValue)
            {
                query = query.Where(a=>a.payment_method == filter.paymentMethod);
            }

            if (filter.status.HasValue)
            {
                query = query.Where(a=>a.status == filter.status.Value);
            }
            if (filter.worker_id.HasValue)
            {
                query = query.Where(a=>a.worker_id == filter.worker_id.Value);
            }

            var payment = await query.ToListAsync();
            if (!payment.Any())
            {
                return new ServiceResponse
                {
                    Success=false,
                    Message= "Payment not found"
                };
                
            }

            return new ServiceResponse
            {
                Success=true,
                Message = "Payment retrived successfully",
                Response = query
            };
        }
    }
}