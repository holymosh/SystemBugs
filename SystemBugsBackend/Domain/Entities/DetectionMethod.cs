using System.Collections.Generic;

namespace Domain.Entities
{
    public class DetectionMethod : BaseEntityWithTitle
    {
        public ICollection<Bug> Bugs { get; set; }
    }
}