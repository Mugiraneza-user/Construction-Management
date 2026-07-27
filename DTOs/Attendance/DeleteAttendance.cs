using System.ComponentModel.DataAnnotations;

namespace mks.Dtos
{
    public class DeleteAttendanceDto
    {
        public int id {get; set;}
        public string? worker_id{get; set;}
    }
}