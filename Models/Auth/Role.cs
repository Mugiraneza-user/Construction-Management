using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;
using mks.Interfaces;
[Table("roles")]
public class Role
{
    public int id {get; set;}

    public string? role_name {get; set;}

    public string? description {get; set;}

    public bool is_active {get; set;}

    public DateTime created_at {get; set;}

}