using System.Collections.Generic;

namespace Domain.Entities
{
    public class SberSystem : BaseEntityWithTitle
    {
        public ICollection<Bug> Bugs { get; set; } = new List<Bug>();
    }
}