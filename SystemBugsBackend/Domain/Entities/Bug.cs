using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Bug : BaseEntity
    {
        public int SystemId { get; set; }
        public SberSystem System { get; set; }
        public string Summary { get; set; }

        public string State { get; set; }
        public string Found { get; set; }

        public string CriticalLevel { get; set; }

        public string DegectType { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ChangeDate { get; set; }

        public DateTime? ClosingDate { get; set; }

        public string DetectionMethod { get; set; }

        public int? ReopensAmount { get; set; }
    }
}
