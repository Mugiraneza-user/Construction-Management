using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using mks.Data;
using mks.Dtos;
using mks.DTOs;
using mks.Interfaces;
using mks.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace mks.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDbContext _context;

        public AttendanceService(ApplicationDbContext context)
        {
            _context= context;
        }

        public async Task<ServiceResponse> CreateAttendanceAsync(CreateAttendanceDto dto)

        {
            var worker =  await _context.workers.FirstOrDefaultAsync(a=>a.worker_number == dto.worker_id);

            if(worker == null)
            return new ServiceResponse
            {
                Success= false,
                Message = "Worker not found"
            } ;
        
           if(worker.status != Enum.StatusType.Active)
           return new ServiceResponse
           {
               Success=false,
               Message= "Worker account is not active please activate it"
           };

            var period = await _context.WorkerPeriods.AnyAsync(a=> a.id == dto.period_id);

           if ( !period)
           return new ServiceResponse
           {
               Success = false,
               Message = "Period not found"
           };
             foreach (var day in dto.days)
                {
                    if (day.value != 0m &&
                        day.value != 0.5m &&
                        day.value != 1m)
                    {
                        return new ServiceResponse
                        {
                            Success = false,
                            Message = $"Invalid attendance value on {day.date}."
                        };
                    }
                }
                var attendance = await _context.Attendances.FirstOrDefaultAsync(x =>  x.period_id == dto.period_id && x.worker_id == dto.worker_id);

                if (attendance == null)
                {
                 attendance = new Attendance
                    {
                        period_id = dto.period_id,
                        worker_id = dto.worker_id,
                        days = dto.days
                    };

                    _context.Attendances.Add(attendance);

                    await _context.SaveChangesAsync();
                     return new ServiceResponse
                    {
                        Success = true,
                        Message = "Attendance created successfully.",
                        Response= attendance

                    };

                    }
                    var existingDays = attendance.days;

                        foreach (var newDay in dto.days)
                        {
                            if (existingDays.Any(d => d.date == newDay.date))
                            {
                                return new ServiceResponse
                                {
                                    Success = false,
                                    Message = $"Attendance for {newDay.date} already exists."
                                };
                            }

                            existingDays.Add(newDay);
                        }

                            attendance.days = existingDays;

                            await _context.SaveChangesAsync();

                            return new ServiceResponse
                            {
                                Success = true,
                                Message = "Attendance updated successfully.",
                                
                            };
         
        } 

        public async Task <ServiceResponse> UpdateAttendanceAsync(UpdateAttendanceDto dto)

        {
            var worker =  await _context.workers.AnyAsync(a=>a.worker_number == dto.worker_id);

            if(!worker )
            return new ServiceResponse
            {
                Success= false,
                Message = "Worker not found"
            } ;

            var period = await _context.WorkerPeriods.AnyAsync(a=> a.id == dto.period_id);

           if ( !period)
           return new ServiceResponse
           {
               Success = false,
               Message = "Period not found"
           };

            var attend = await _context.Attendances.FirstOrDefaultAsync(x => x.id == dto.id);

            if (attend == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Attendance not found."
                };
            }
            

            foreach (var day in dto.days)
                {
                    if (day.value != 0m &&
                        day.value != 0.5m &&
                        day.value != 1m)
                    {
                        return new ServiceResponse
                        {
                            Success = false,
                            Message = $"Invalid attendance value on {day.date}."
                            
                        };
                    }
                    
                }

                attend.days = dto.days;
                _context.Attendances.Update(attend);
                await _context.SaveChangesAsync();

                return new ServiceResponse
                {
                    Success = true,
                    Message = "Attendance updated successfully."
                };
        }

        public async Task<ServiceResponse> DeleteAttendanceAsync(DeleteAttendanceDto dto)
        {
             var attendance = await _context.Attendances.FirstOrDefaultAsync(a => a.id == dto.id);

                if (attendance == null)
                {
                    return new ServiceResponse
                    {
                        Success = false,
                        Message = "Attendance not found."
                    };
                }

                _context.Attendances.Remove(attendance);
                await _context.SaveChangesAsync();

                return new ServiceResponse
                {
                    Success = true,
                    Message = "Attendance deleted successfully."
                };
        }
        public async Task<ServiceResponse> FilterAttendanceAsync(FilterAttendanceDto filter)
        {
             var query = _context.Attendances.AsQueryable();

                if (filter.period_id.HasValue)
                {
                    query = query.Where(a => a.period_id == filter.period_id.Value);
                }

                if (!string.IsNullOrEmpty(filter.worker_id))
                {
                    query = query.Where(a=>a.worker_id.Contains(filter.worker_id));
                }

                var attendance = await query.ToListAsync();

                    if(!attendance.Any())
                    return new ServiceResponse
                    {
                        Success=false,
                        Message = "No attendance found.",
                    };

                    return new ServiceResponse
                    {
                        Success = true,
                        Message =  "Attendance retrieved successfully.",
                        Response = attendance
                    };
        }
        public async Task<ServiceResponse> GetAttendanceAsync()
        {
            var attendance = await _context.Attendances.ToListAsync();
                   

            return new ServiceResponse
            {
                Success= true,
                Message= "Attendance dsiplayed successfully",
                Response = attendance
            };
        }

        public async Task <ServiceResponse> GetAttendanceByIdAsync(GetAttendanceByIdDto filter)
        {
            var attendance = await _context.Attendances.FirstOrDefaultAsync(a=>a.id == filter.id);

            if(attendance == null)
            return new ServiceResponse
            {
                Success= false,
                Message = "Attendance Id not found"
            };

            var query = _context.Attendances.AsQueryable();

            if(filter.id.HasValue)
            {
                query = query.Where(a=>a.id == filter.id.Value);
            }

             var attendances = await query.ToListAsync();

             if(!attendances.Any())
             return new ServiceResponse
             {
                 Success=false,
                 Message = "id not found"
             };
             return new ServiceResponse
             {
                 Success = true,
                 Message = "Attendance retrived successfully",
                 Response = attendances
             };

        }

        
    }
}