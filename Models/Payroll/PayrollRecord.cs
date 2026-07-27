using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace mks.Models{

    [Table("payroll_record")]
    public class Payroll
{
    public int id {get; set;}
    [ForeignKey("worker_id")]
    public int worker_id {get; set;}

   [ForeignKey("period_id")]
    public int period_id {get; set;}

    public string? worker_name {get; set;}

    public string? category {get; set;}

    public int days_worked { get; set;}

    public int salary_per_days{get; set;}

    public int deductions {get; set;}

    public int net_salary {get; set;}
    


}
}