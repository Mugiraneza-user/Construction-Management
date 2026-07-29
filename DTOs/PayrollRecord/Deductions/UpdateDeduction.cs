using System.ComponentModel.DataAnnotations;

namespace mks.Dtos
{
    
    public class UpdateDeductionDto
    {
        public int id {get; set;}
        public int worker_id{get; set;}
        public decimal? amount {get; set;}

        public string? reason {get; set;}
    }
}