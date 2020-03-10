using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Config
{
    public class EventConfiguration : BaseEntityTypeConfiguration<Event>
    {

        public override void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            base.Configure(builder);

            builder.Property<string>(p => p.Name)
                .HasMaxLength(400)
                .IsRequired();
        }
    }
}

