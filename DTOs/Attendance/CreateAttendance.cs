using System.ComponentModel.DataAnnotations;


namespace mks.Dtos
{
    public class CreateAttendanceDto
    {
        public string? worker_id {get; set;}

        public int period_id {get; set;}


        public List<AttendanceDayDto> days { get; set; } = new();
    }
}