using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mks.Models
{
    [Table("creditor")]
    public class Creditors
    {
        public int id {get; set;}
        public string? name {get; set;}

        public string? phone {get; set;}

        public string? notes {get; set;}

        public DateTime created_at {get; set;}
    }
}