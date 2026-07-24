using System.ComponentModel.DataAnnotations;
using mks.Enum;

namespace mks.DTOs
{
    public class WorkerCategoryFilterDto
    {
        public int? id {get; set;}

        public int? salary_per_day{get; set;}

        public int? hour_per_day{get; set;}

        public WageType? wage_type{get; set;}
        
        public string? name {get; set;}
    }
}