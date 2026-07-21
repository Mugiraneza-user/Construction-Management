using System.ComponentModel.DataAnnotations;


namespace mks.DTOs;

public class UpdateRolesDto
{  
     [Required]
    public  int id {get; set;}

    [Required]
    public  required int role_id {get; set;}
}