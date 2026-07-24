using System.ComponentModel.DataAnnotations;
using mks.Enum;

namespace mks.DTOs{
    public class FilterWorkerDto
{
    public string? worker_number {get; set;}
    public string? national_id {get; set;}

    public string? full_name{get; set;}

    public string? telephone {get; set;}

    public int? category_id {get; set;}

    public string? bank_account {get; set;}
    public DateTime? date_joined {get; set;}

    public ShifyType? shift {get; set;}

    public StatusType? status {get; set;}


}

}