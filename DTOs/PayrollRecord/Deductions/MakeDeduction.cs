using System.ComponentModel.DataAnnotations;
using mks.Enum;

namespace mks.DTOs
{
    public class MakeDeductionDto
    {
        public int id {get;set;}

        public int creditor_id {get; set;}

        public required int worker_id{get; set;} 
        public decimal amount {get; set;}

        public required string? reason{get; set;}

        public DateTime created_at {get; set;}


        
    }
}