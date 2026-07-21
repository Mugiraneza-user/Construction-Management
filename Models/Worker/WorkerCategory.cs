using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

[Table("worker_category")]

public class WorkerCategory
{
    public string? name {get; set;}
    public int salary_per_day{ get ; set;}

    public int hours_per_day{get; set;}
}