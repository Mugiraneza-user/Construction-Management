using System.ComponentModel.DataAnnotations;
using mks.Enum;

namespace mks.DTOs{
    public class CreateWorkerDto
{
    public int national_id {get; set;} 

    public required string full_name{get; set;}

    public required int telephone {get; set;}

    public int category_id {get; set;}

    public string bank_account {get; set;} = string.Empty;
    public DateTime date_joined {get; set;}

    public ShifyType shift {get; set;}

    public StatusType status {get; set;}


}

}