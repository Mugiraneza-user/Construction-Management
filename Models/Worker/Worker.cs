using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Identity.Client;
using mks.Enum;

namespace mks.model;

public class Workers
{
    
 public int id {get ; set ;}

 public required string? worker_number{get ; set ;} 

 public required string? national_id{get; set;}
 public string? full_name{get ; set;}

 public string? telephone{get; set;}
 [ForeignKey("category_id")]
 public int category_id {get; set;}

public int salary_per_day {get; set;}

public WageType wage_type {get; set;}

public string? bank_account {get; set;}

public DateTime date_joined {get; set;}

public StatusType status {get; set;}

public ShifyType shift {get; set;}
}
