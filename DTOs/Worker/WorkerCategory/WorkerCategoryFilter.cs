using System.ComponentModel.DataAnnotations;

namespace mks.DTOs
{
    public class WorkerCategoryFilterDto
    {
        public int? id {get; set;}

        public int? salary_per_day{get; set;}

        public int? hour_per_day{get; set;}
        
        public string? name {get; set;}
    }
}