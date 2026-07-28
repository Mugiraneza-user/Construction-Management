using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;
using mks.Enum;
using mks.Models;

namespace mks.model;
[Table("worker")]
public class Worker
{
    
 public int id {get ; set ;}

 public  string? worker_number{get ; set ;} 

 public string? national_id{get; set;}
 public string full_name{get ; set;} = string.Empty;

 public string? telephone{get; set;}
 [ForeignKey("catrgory_id")]
 public int category_id {get; set;}

public string? bank_account {get; set;}

public DateTime date_joined {get; set;}

public StatusType status {get; set;}

public ShifyType shift {get; set;}

 public WorkerCategory Category { get; set; } = null!;
}
