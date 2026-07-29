using System.ComponentModel.DataAnnotations;
using mks.Enum;


namespace mks.DTOs
{
    public class MarkAsPaidDeductionDto
    {
        public int id {get; set;}

        public required int worker_id{get; set;}

        public decimal? amount {get; set;}

        public PaymentStatus status {get;set;}
    }
}