using System.ComponentModel.DataAnnotations;

namespace mks.Dtos
{
    public class UpdateAttendanceDto{

    public int id {get; set;}   
    public int period_id {get; set;}
    public string? worker_id {get; set;}

     public List<AttendanceDayDto> days { get; set; } = new();
}

 }