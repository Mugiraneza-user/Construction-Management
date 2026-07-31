using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using mks.Data;
using mks.Dtos;
using mks.DTOs;
using mks.Enum;
using mks.Interfaces;
using mks.model;


namespace mks.Services
{
    public class DeductionService : IDeductionService
    {
        private readonly ApplicationDbContext _context;

        public DeductionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse> MakeDeductionAsync(MakeDeductionDto dto)
        {
            var worker = await _context.workers.FirstOrDefaultAsync(a=>a.id == dto.worker_id);

            if (worker == null)
            return new ServiceResponse
            {
                Success=false,
                Message = "Worker not found"
            };

            var creditor = await _context.Creditor.FirstOrDefaultAsync(a=>a.id == dto.creditor_id);

            if(creditor == null)
            return new ServiceResponse
            {
                Success = false,
                Message = "Creditor not found"
            };

           

            var initialPayment = await _context.payrolls.FirstOrDefaultAsync(a=>a.worker_id == dto.worker_id);

            if(initialPayment.net_salary < dto.amount)
            return new ServiceResponse
            {
                Success=false,
                Message = "Worker has less amout of money for being deducted"
            };
            decimal makededuction = initialPayment.net_salary- dto.amount;

            var deduction = new Deduction
            {
                worker_id = dto.worker_id,
                creditor_id=dto.creditor_id,
                reason= dto.reason,
                amount = dto.amount,
                created_at = dto.created_at,
                status = PaymentStatus.pending
                

            };
            _context.deductions.Add(deduction);

            await _context.SaveChangesAsync();
            return new ServiceResponse
            {
                Success = true,
                Message = "Deduction applied successfully",
                Response = deduction
            };
            
        }

        public async Task<ServiceResponse> MarkAsPaidDeductionAsync(MarkAsPaidDeductionDto dto)
        {
             var worker = await _context.workers.FirstOrDefaultAsync(a=>a.id == dto.worker_id);

            if (worker == null)
            return new ServiceResponse
            {
                Success = false,
                Message= "Worker not found"
            };
            var deduction = await _context.deductions.FirstOrDefaultAsync(a=>a.id == dto.id);

            if (deduction == null)
            return new ServiceResponse
            {
                Success = false,
                Message ="Debt not found"
            };

            if (deduction.status == Enum.PaymentStatus.paid)
            return new ServiceResponse
            {
                Success = false,
                Message = "Dept already paid"
            };

            deduction.status = PaymentStatus.paid;

             await _context.SaveChangesAsync();

             return new ServiceResponse
             {
                 Success = true,
                 Message = "Dept is paid successfully"
             };
        }

        public async Task <ServiceResponse> DeleteDeductionAsync(DeleteDeduction dto)
        {

            var worker = await _context.workers.AnyAsync(a=>a.id == dto.id);

            if (!worker )
            return new ServiceResponse
            {
                Success = false,
                Message =" worker not found"
            };
           
            
            var deduction = await _context.deductions.FirstOrDefaultAsync(a=>a.id == dto.id && a.amount == dto.amount);

            if(deduction.status == PaymentStatus.paid)
            return new ServiceResponse
            {
                Success = false,
                Message="Cannot delete paid deduction"
            }; 

            if(deduction == null)
            return new ServiceResponse
            {
                Success = false,
                Message = "Debt infomation not match"
            };

            _context.deductions.Remove(deduction);

            await _context.SaveChangesAsync();

            return new ServiceResponse{
                Success = true,
                Message = "debit deleted successfully"
            };
        }

        public async Task <ServiceResponse> GetDeductionAsync()
        {
            var deduction = await _context.deductions.ToListAsync();

            return new ServiceResponse
            {
                Success = true,
                Message= "Deduction retived successfully",
                Response = deduction
            };
        }

        public async Task<ServiceResponse> UpdateDeductionAsync(UpdateDeductionDto dto)
        {
             var deduction = await _context.deductions.FirstOrDefaultAsync(a=>a.id == dto.id);

             if(deduction == null)
             return new ServiceResponse
             {
                 Success = false,
                 Message = "Debt not found"

             };

             if (deduction.status == PaymentStatus.paid)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Paid deductions cannot be updated."
                };
            }

               deduction.amount = dto.amount.Value;
               deduction.reason = dto.reason;
          

            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                Success = true,
                Message = "Debt updated successfully"
                    };
                }
    }
} 