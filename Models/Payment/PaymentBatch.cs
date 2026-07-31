using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using mks.Enum;

namespace mks.Models
{
    [Table("payment_batch")]
    public class PaymentBatch
{
    public int id { get; set; }

    public string batch_number { get; set; } = string.Empty;

    public int period_id { get; set; }

    public int? category_id { get; set; }

    public decimal total_amount { get; set; }

    public PaymentMethod payment_method { get; set; }

    public DateTime payment_date { get; set; }

    public PaymentBatchStatus status { get; set; }

    public string? notes { get; set; }


    public WorkerPeriod period { get; set; } = null!;

    public WorkerCategory? category { get; set; }

    public ICollection<Payment> payments { get; set; } = new List<Payment>();
}
}