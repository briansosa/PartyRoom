using System;

namespace Database
{
    interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreatedOn { get; set; }
        bool IsDeleted { get; set; }
    }
}