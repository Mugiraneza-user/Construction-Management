using System.ComponentModel.DataAnnotations;
using Microsoft.Net.Http.Headers;

namespace mks.DTOs
{
    public class AddWorkerCategoryDto
    {
        
        public  required string? name  {get; set;}

        public required int salary_per_day {get; set;}
        public required int hours_per_day {get; set;}

        public bool is_active {get; set;}

    }
}