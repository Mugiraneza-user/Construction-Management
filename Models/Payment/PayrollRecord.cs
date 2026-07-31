using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using mks.Enum;
using mks.model;

namespace mks.Models{

    [Table("payroll_record")]
    public class Payroll
{
    public int id {get; set;}
    [ForeignKey("worker_id")]
    public int worker_id {get; set;}

   [ForeignKey("period_id")]
    public int period_id {get; set;}

    public decimal days_worked { get; set;}

    public decimal deductions {get; set;}
     
     public decimal gross_salary {get; set;}
    public decimal net_salary {get; set;}
    public PaymentStatus status {get;set;}

    public Worker worker { get; set; } = null!;

    public WorkerPeriod Period { get; set; } = null!;
}
}