using System.ComponentModel.DataAnnotations;

namespace mks.DTOs
{
    public class RoleFilterDto
    {
        public int? id { get; set;}
        public string? role_name {get;set;}
        public DateTime? created_at {get; set;}

        public bool? is_active { get; set;}
    }
}