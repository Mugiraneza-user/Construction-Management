using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using mks.Enum;
using mks.model;

namespace mks.Models
{
    [Table("payment")]
    public class Payment
{
    public int id { get; set; }

    public string payment_number { get; set; } = string.Empty;

    public PaymentMethod payment_method { get; set; }

    public int payroll_id { get; set; }

    public int worker_id { get; set; }

    public decimal amount { get; set; }

    public PaymentStatus status { get; set; }
    public int period_id {get; set;}

    public string? notes { get; set; }
    public DateTime Payment_date {get; set;}

    public Payroll payroll { get; set; } = null!;

    public Worker worker { get; set; } = null!;

    public WorkerPeriod period{get; set;}
}
}