using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace mks.model;

public class Workers
{
    
 public int id {get ; set ;}

 public required int worker_number{get ; set ;} 

 public required int national_id{get; set;}
 public string? full_name{get ; set;}

 
}
