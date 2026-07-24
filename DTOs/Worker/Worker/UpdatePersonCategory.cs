using System.ComponentModel.DataAnnotations;

namespace mks.DTOs
{
    public class UpdatePersonCategoryDto
    {
        public string? work_number{get; set;}

        public int newcategory_id{get;set;}
    }
}