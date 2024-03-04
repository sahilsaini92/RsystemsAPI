using Microsoft.EntityFrameworkCore;
using RSystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsystemsAssignment.Data
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Appointment> Appointment { get; set; }

        // Constructor accepting DbContextOptions
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasNoKey();
            modelBuilder.Entity<Client>().HasNoKey();
            modelBuilder.Entity<Appointment>().HasNoKey();
        }

    }
}
