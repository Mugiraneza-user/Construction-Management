using System.ComponentModel.DataAnnotations;
using mks.Enum;


namespace mks.DTOs
{
    public class UpdateWorkerPeriodDto
    {
        public int id {get; set;}
        public string name {get; set;} = string.Empty;

        public DateTime start_date {get; set;}

        public DateTime end_date {get; set;}

        public PeriodStatus status{get; set;}
    }
}