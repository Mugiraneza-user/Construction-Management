using System.ComponentModel.DataAnnotations;



namespace mks.DTOs
{
  public class CreateRoleDto
    {
        public string? role_name {get; set;}

        public string? description {get; set;}

        public DateTime created_at {get; set;}

        public bool is_active{get; set; }
        
    }    
}