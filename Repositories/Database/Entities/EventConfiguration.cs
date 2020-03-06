using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Config
{
    public class EventConfiguration : BaseEntityTypeConfiguration<Event>
    {

        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(p => p.Id);

            base.Configure(builder);

            builder.Property<string>(p => p.Name)
                .HasMaxLength(400)
                .IsRequired();
        }
    }

    //public class Event : IBaseEntity
    //{
    //    [Key]
    //    public int Id { get; set; }
    //    public DateTime CreatedOn { get; set; } = DateTime.Now;
    //    public bool IsDeleted { get; set; } = false;    
    //}
}

