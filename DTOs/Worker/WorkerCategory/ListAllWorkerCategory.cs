using System.ComponentModel.DataAnnotations;

namespace mks.DTOs
{
    public class ListAllWorkerCategoryDto
    {
        public int id {get; set;}
        [Required]
        public string name {get; set;} = string.Empty;

         public int salary_per_day{get; set;}
        public int hour_per_day {get; set;}

        public bool is_active{get; set;}
    }
}