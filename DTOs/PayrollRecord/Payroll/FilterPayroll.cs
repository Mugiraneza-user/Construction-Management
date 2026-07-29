using System.ComponentModel.DataAnnotations;

namespace mks.DTOs{



public class PayrollFilterDto
{
    public int? period_id { get; set; }

    public int? worker_id { get; set; }
}

}