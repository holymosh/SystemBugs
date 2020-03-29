using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Bug : BaseEntity
    {
        [ForeignKey("System")]
        public int SystemId { get; set; }
        public SberSystem System { get; set; }
        public string Summary { get; set; }
        [ForeignKey("State")]
        public int StateId { get; set; }
        public State State { get; set; }
        public string Found { get; set; }

        [ForeignKey(nameof(CriticalLevel))]
        public int CriticalLevelId { get; set; }
        public CriticalLevel CriticalLevel { get; set; }

        [ForeignKey(nameof(DefectType))]
        public int DefectTypeId { get; set; }
        public DefectType DefectType { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ChangeDate { get; set; }

        public DateTime? ClosingDate { get; set; }

        public int DetectionMethodId { get; set; }
        public DetectionMethod DetectionMethod { get; set; }

        public int? ReopensAmount { get; set; }
    }
}
