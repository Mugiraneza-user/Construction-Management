using System.ComponentModel.DataAnnotations;
using mks.Enum;

namespace mks.Dtos
{
    public class CreatePersonPaymentDto
{

    public int payroll_id { get; set; }

    public int period_id{get;set;}

    public int worker_id { get; set; }

    public PaymentMethod paymentMethod{get; set;}

    public string? notes{get; set;}
}
}