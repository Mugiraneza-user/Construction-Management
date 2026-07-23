using System.ComponentModel.DataAnnotations;
using mks.Enum;

namespace mks.DTOs
{
    public class UpdateWorkerPeriodStatusDtoo
    {
        public int id {get; set;}
        public PeriodStatus status {get; set;}
    } 
}