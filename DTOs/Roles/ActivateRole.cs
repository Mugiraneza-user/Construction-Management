using System.ComponentModel.DataAnnotations;

namespace mks.DTOs
{
    
    public class ActivateRoleDto
    {
        public required int role_id {get; set;}

        public bool is_active {get; set;}
    }
}