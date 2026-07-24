using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;
using mks.Enum;

namespace mks.model;
[Table("worker")]
public class Worker
{
    
 public int id {get ; set ;}

 public  string? worker_number{get ; set ;} 

 public int national_id{get; set;}
 public string full_name{get ; set;} = string.Empty;

 public int telephone{get; set;}
 [ForeignKey("category_id")]
 public int category_id {get; set;}

public string? bank_account {get; set;}

public DateTime date_joined {get; set;}

public StatusType status {get; set;}

public ShifyType shift {get; set;}
}
