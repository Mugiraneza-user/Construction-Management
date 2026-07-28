using System.ComponentModel.DataAnnotations;

namespace mks.DTOs
{
    

public class UpdatePayrollDto
{
    public int id { get; set; }

    public decimal deductions { get; set; }
}
}