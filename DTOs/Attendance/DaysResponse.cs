using System.ComponentModel.DataAnnotations;
using mks.Dtos;

namespace mks.DTOs
{
    public class DayResponse
    {
        // public bool Success {get; set;}

        public int id { get; set;}
        public string worker_id { get; set; } = string.Empty;
        public int period_id { get; set; }

       public List<AttendanceDayDto> days { get; set; } = new();
    }
}