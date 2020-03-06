using System;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Database.Config;
using System.Reflection;

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
    }
}