using System.ComponentModel.DataAnnotations;

namespace mks.DTOs
{
    public class UpdateCreditorDto
    {
         public int id {get; set;}
        public string? name {get; set;}
        public string? phone {get; set;}
        public string? notes {get; set;}

        
    }
}