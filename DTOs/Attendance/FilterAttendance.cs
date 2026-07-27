using System.ComponentModel.DataAnnotations;

namespace mks.Dtos
{
    public class FilterAttendanceDto
    {
        public string? worker_id {get;  set;}
        public string? worker_name { get; set;}
        public int? period_id { get; set;}


    }
}