using System.ComponentModel.DataAnnotations;

namespace mks.DTOs
{
    public class DeleteDeduction
    {
        public int id {get; set;}
        public required string worker_id{get; set;}
        public required decimal amount {get; set;}


    }
}