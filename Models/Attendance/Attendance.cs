using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using mks.Dtos;

namespace mks.Models
{
    [Table("attendance")]
    public class Attendance
    {
        public int id {get; set;}
        [ForeignKey ("period_id")]
        public int period_id {get; set;}
        [ForeignKey("worker_id")]
        public String? worker_id{get; set;}

           public List<AttendanceDayDto> days { get; set; } = new();


    }
}