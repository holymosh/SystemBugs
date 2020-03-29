using System.Collections.Generic;

namespace Domain.Entities
{
    public class DefectType : BaseEntityWithTitle
    {
        public ICollection<Bug> Bugs { get; set; } = new List<Bug>();
    }
}