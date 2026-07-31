using System.ComponentModel.DataAnnotations;
using mks.Enum;

namespace mks.Dtos
{
    public class FilterBatchPaymentDto
    {
        public int? id {get; set;}
        public int? period_id{get; set;}

        public int? category_id{get;set;}

        public PaymentStatus? status {get; set;}

        public PaymentMethod? paymentMethod{get; set;}

        public DateTime? payment_date{get;set;}

    
    }
}