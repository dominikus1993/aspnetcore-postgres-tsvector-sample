using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Core.Model;

namespace ToDoList.Api.Infrastructure.EntityFramework;

public sealed class ToDoDbContext(DbContextOptions<ToDoDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDos>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.HasMany(x => x.Items).WithOne().HasForeignKey("todo_list_id");
        });
        base.OnModelCreating(modelBuilder);
    }
}