using System.ComponentModel.DataAnnotations;
using Microsoft.Net.Http.Headers;
using mks.Enum;

namespace mks.DTOs
{
    public class AddWorkerCategoryDto
    {
        
        public  required string? name  {get; set;}

        public required decimal salary_per_day {get; set;}
        public required int hours_per_day {get; set;}

        public WageType wage_type{get;set;}

        public bool is_active {get; set;}

    }
}