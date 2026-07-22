using System.ComponentModel.DataAnnotations;

namespace mks.DTOs
{
    public class DeactivateWorkerCategoryDto
    {
        public int id {get; set;}
        // public string? name {get; set;}

        public bool is_active{get; set;}
    }
}