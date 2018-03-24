using System;

namespace Models
{
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
        string Name { get; set; }
        DateTime CreatedAt { get; set; }
        Nullable<DateTime> UpdatedAt { get; set; }
        Nullable<DateTime> DeletedAt { get; set; }
    }
}