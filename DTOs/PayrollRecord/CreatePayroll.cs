using System.ComponentModel.DataAnnotations;

namespace mks.DTOs
{
    public class CreatePayrollDto
{
    public int period_id { get; set; }

    public int worker_id { get; set; }

    public decimal deductions { get; set; }
}
}
