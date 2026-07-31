using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using mks.Enum;
using mks.Models;

namespace mks.model
{
    [Table("worker_deduction")]
    public class Deduction
    {
       public int id {get; set;}
       public int worker_id {get;set;} 

       public int creditor_id {get; set;}

       public decimal amount {get;set;}

       public string reason {get; set;} = string.Empty;

       public DateTime created_at{get;set;}

       public DateTime updated_at{get;set;}
       public PaymentStatus status {get; set;}

       public Worker worker {get; set;}

       public Creditors creditors{get; set;}

    }
   
}