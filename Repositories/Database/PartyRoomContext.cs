using System;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Database.Config;
using System.Reflection;
using System.Linq;
using Common.Functional;

namespace Database
{
    public class PartyRoomContext : DbContext
    {
        public PartyRoomContext(DbContextOptions<PartyRoomContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Gets and starts all the configurations of the classes that use the IEntityTypeConfiguration interface for creating tables
            Assembly assemblyWithConfigurations = GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);
        }

        public override int SaveChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries()
                .Where(e => e.Properties.Any(p => p.Metadata.Name == "UpdateDateTime")
                         && e.State != Microsoft.EntityFrameworkCore.EntityState.Added))
            {
                entry.Property("UpdateDateTime").CurrentValue = DateTime.Now;
            }

            return base.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            var entry = this.Entry(entity);
            // TODO: check entry.State
            if (entry.Properties.Any(p => p.Metadata.Name == "IsDeleted"))
            {
                entry.Property("UpdateDateTime").CurrentValue = DateTime.Now;
                entry.Property("IsDeleted").CurrentValue = true;
            }
            else
            {
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            }
        }
    }
}