using System.ComponentModel.DataAnnotations;


namespace mks.DTOs
{
    public class DeactivateRoleDto
    {
        public required int role_id {get; set;}

        public bool is_activate {get; set;}
    }
}