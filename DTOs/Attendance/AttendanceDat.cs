using System.ComponentModel.DataAnnotations;
namespace mks.Dtos
{
    

public class AttendanceDayDto
{
    public DateOnly date { get; set; }

    public decimal value { get; set; } // 0, 0.5, 1
}
}