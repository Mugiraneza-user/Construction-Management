using System.ComponentModel.DataAnnotations;
using mks.Enum;

namespace mks.Dtos
{
    public class FilterPaymentDto
    {
        public int? id {get; set;}

        public PaymentStatus? status {get; set;}

        public PaymentMethod? paymentMethod{get; set;}

        public DateTime? payment_date{get;set;}

        public int? worker_id {get; set;}
    }
}