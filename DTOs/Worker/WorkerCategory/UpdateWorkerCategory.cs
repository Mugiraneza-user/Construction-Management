using System.ComponentModel.DataAnnotations;
using mks.Enum;


namespace mks.DTOs
{
    
    public class UpdateWorkerCategoryDto
    {
        public int id {get; set;}
        public  required string name  {get; set;}

        public required int salary_per_day {get; set;}
        public required int hours_per_day {get; set;}

        public WageType wage_type{get;set;}

        public bool is_active {get; set;}
    }
}