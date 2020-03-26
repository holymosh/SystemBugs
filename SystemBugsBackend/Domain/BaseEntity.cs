using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
    }

    public abstract class BaseEntityWithTitle : BaseEntity
    {
        public string Title { get; set; }
    }
}
