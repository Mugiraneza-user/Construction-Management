using System.ComponentModel.DataAnnotations;

namespace mks.DTOs
{
    public class DeleteWorkerDto
    {
        public string? worker_number{get; set;}

        public string full_name{get; set;}= string.Empty;
    }
}