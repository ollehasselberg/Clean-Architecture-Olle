using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace InfrastructureLayer.Data
{
    // EF Core-kopplingen mot SQL Server, konfigureras och registreras i API/Program.cs
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1-till-många: en TodoList har flera TodoItems, kopplade via TodoListId (FK)
            modelBuilder.Entity<TodoList>()
                .HasMany(x => x.Items)
                .WithOne(x => x.TodoList)
                .HasForeignKey(x => x.TodoListId);
        }
    }
}