using System.ComponentModel.DataAnnotations;
using mks.Data;
using mks.Interfaces;
using mks.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using mks.Models;
using mks.Dtos;

namespace mks.Services
{
    public class PayRollService : IPayRollService
    {
        private readonly ApplicationDbContext _context;

        public PayRollService(ApplicationDbContext context)
        {
            _context = context;
        }
       public async Task<ServiceResponse> CreatePayrollAsync(CreatePayrollDto dto)
        {
            var period = await _context.WorkerPeriods.FirstOrDefaultAsync(x => x.id == dto.period_id);

            if (period == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Work period not found."
                };
            }

            var worker = await _context.workers.Include(x => x.Category).FirstOrDefaultAsync(x => x.id == dto.worker_id);

            if (worker == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Worker not found."
                };
            }

            bool payrollExist = await _context.payrolls.AnyAsync(x =>x.period_id == dto.period_id && x.id == dto.worker_id);

            if (payrollExist)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Payroll already generated."
                };
            }

            var attendance = await _context.Attendances.FirstOrDefaultAsync(x =>x.period_id == dto.period_id && x.worker_id == worker.worker_number);
            Console.WriteLine($"Period ID: {dto.period_id}");
                Console.WriteLine($"Worker ID from worker: {worker.id}");
                Console.WriteLine($"Worker ID as string: {worker.id.ToString()}");

            if (attendance == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Attendance not found."
                };
            }

            var attendanceDays = JsonSerializer.Deserialize<List<AttendanceDayDto>>(attendance.days)?? new List<AttendanceDayDto>();

            decimal daysWorked = attendanceDays.Sum(x => x.value);

            decimal salaryPerDay = worker.Category.salary_per_day;

            decimal grossSalary = daysWorked * salaryPerDay;

            decimal netSalary = grossSalary - dto.deductions;

            var payroll = new Payroll
            {
                worker_id = worker.id,
                period_id = dto.period_id,
                days_worked = daysWorked,
                gross_salary = grossSalary,
                deductions = dto.deductions,
                net_salary = netSalary
            };

            _context.payrolls.Add(payroll);
            Console.WriteLine($"WorkerId = {payroll.worker_id}");
            Console.WriteLine($"PeriodId = {payroll.period_id}");

            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                Success = true,
                Message = "Payroll generated successfully."
            };
        }

        public async Task<ServiceResponse> UpdatePayrollAsync(UpdatePayrollDto dto)
        {
            var payroll = await _context.payrolls.FirstOrDefaultAsync(x => x.id == dto.id);

            if (payroll == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Payroll not found."
                };
            }

            payroll.deductions = dto.deductions;

            payroll.net_salary = payroll.gross_salary - payroll.deductions;

            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                Success = true,
                Message = "Payroll updated successfully."
            };
        }

        public async Task<ServiceResponse> DeletePayrollAsync(int id)
        {
            var payroll = await _context.payrolls.FirstOrDefaultAsync(x => x.id == id);

            if (payroll == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Payroll not found."
                };
            }

            _context.payrolls.Remove(payroll);

            await _context.SaveChangesAsync();

            return new ServiceResponse
            {
                Success = true,
                Message = "Payroll deleted successfully."
            };
        }

        public async Task<ServiceResponse> GetPayrollAsync()
        {
            var payroll = await _context.payrolls.ToListAsync();

            return new ServiceResponse
            {
                Success = true,
                Message = "Payroll retrieved successfully.",
                Response = payroll
            };
        }

        public async Task<ServiceResponse> FilterPayrollAsync(PayrollFilterDto filter)
        {
            var query = _context.payrolls.Include(x => x.worker).ThenInclude(x => x.Category).AsQueryable();

            if (filter.period_id.HasValue)
            {
                query = query.Where(x => x.period_id == filter.period_id.Value);
            }

            if (filter.worker_id.HasValue)
            {
                query = query.Where(x=> x.worker.id ==filter.worker_id.Value);
            }

            var payroll = await query.ToListAsync();
            
            if(!payroll.Any())
            return new ServiceResponse
            {
                Success= false,
                Message= "Pyroll not found"
            };
            return new ServiceResponse
            {
                Success = true,
                Message = "Payroll found",  
                Response = payroll
            };
        }
    }
}