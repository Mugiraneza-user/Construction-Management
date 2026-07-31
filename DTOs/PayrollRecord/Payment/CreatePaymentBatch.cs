using System.ComponentModel.DataAnnotations;
using mks.Enum;

namespace mks.DTOs
{
    public class CreatePaymentBatchDto
{
    public int period_id { get; set; }

    public int? category_id { get; set; }

    public PaymentMethod payment_method { get; set; }

    public string? notes { get; set; }
}
}