using System;
using System.ComponentModel.DataAnnotations;
using Domain;
using Database;

namespace DatabaseEntity
{
    public class Event : Domain.Event, IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;    
    }
}