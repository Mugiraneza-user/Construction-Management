using System.ComponentModel.DataAnnotations;


namespace mks.DTOs
{
    
    public class RenameWorkerCategoryDto
    {
        
        public int id {get; set;}



        [Required]
        public string name {get; set;}= string.Empty;
    }
}