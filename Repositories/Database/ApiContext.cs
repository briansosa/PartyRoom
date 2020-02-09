using System;
using Microsoft.EntityFrameworkCore;
using DatabaseEntity;

namespace Database
{
    public class ApiContext : DbContext
    {
        public ApiContext (DbContextOptions<ApiContext> options) : base(options){ }

        public DbSet<Event> Events { get; set; }
    }
}