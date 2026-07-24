using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;
using mks.Enum;

[Table("worker_category")]

public class WorkerCategory
{
     public int id {get; set;}
    public string name {get; set;} = string.Empty;
    public int salary_per_day{ get ; set;}

    public int hours_per_day{get; set;}

    public bool is_active{get; set;}

    public WageType wage_type{get;set;}
}