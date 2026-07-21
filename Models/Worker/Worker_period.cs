using System.ComponentModel.DataAnnotations.Schema;
using mks.Enum;


[Table("worker_period")]

public class WorkerPeriod
{
    public string? name {get; set;}
    public DateTime start_date {get; set;}
   public DateTime end_date {get; set;} 
    public PeriodStatus status {get; set;}
}