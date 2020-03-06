using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Database.Config
{
    //public abstract class BaseEntity
    //{
    //    public int Id { get; set; }
    //    public DateTime CreatedOn { get; set; }
    //    public bool IsDeleted { get; set; } = false;

    //    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    //    {
    //        builder.HasKey(p => p.Id);
    //        builder.Property<DateTime>(p => p.CreatedOn)
    //            .HasDefaultValue(DateTime.Now);
    //        builder.Property<DateTime>(p => p.CreatedOn)
    //            .HasDefaultValue(DateTime.Now);

    //    }

    //}

    public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property<bool>("IsDeleted")
                .IsRequired()
                .HasDefaultValue(false);
            builder.Property<DateTime>("InsertDateTime")
                .IsRequired()
                .HasDefaultValueSql("SYSDATETIME()")
                .ValueGeneratedOnAdd();
            builder.Property<DateTime>("UpdateDateTime")
                .IsRequired()
                .HasDefaultValueSql("SYSDATETIME()")
                .ValueGeneratedOnAdd();
        }
    }
}