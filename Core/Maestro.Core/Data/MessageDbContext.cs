﻿namespace Core.Data
{
    using Configuration;
    using Core.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    public abstract class MessageDbContext : DbContext
    {
        protected MessageDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        protected abstract Assembly ConfigurationsAssembly { get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MessageConfiguration());

            builder.ApplyConfigurationsFromAssembly(ConfigurationsAssembly);

            base.OnModelCreating(builder);
        }
    }
}