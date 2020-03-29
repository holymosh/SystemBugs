using System.Collections.Generic;

namespace Domain.Entities
{
    public class CriticalLevel : BaseEntityWithTitle
    {
        public ICollection<Bug> Bugs { get; set; }
    }
}