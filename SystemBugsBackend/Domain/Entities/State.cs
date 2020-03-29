using System.Collections.Generic;

namespace Domain.Entities
{
    public class State : BaseEntityWithTitle
    {
        public ICollection<Bug> Bugs { get; set; }
    }
}