using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Core.Model;

namespace ToDoList.Api.Infrastructure.EntityFramework;

public sealed class ToDoDbContext(DbContextOptions<ToDoDbContext> options) : DbContext(options)
{
    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
    public DbSet<ToDos> ToDos => Set<ToDos>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDos>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()")
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.HasMany(x => x.Items).WithOne().HasForeignKey("todo_list_id");
        });
        modelBuilder.Entity<ToDoItem>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()")
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.IsComplete).IsRequired();
            builder.HasOne<ToDos>().WithMany(x => x.Items).HasForeignKey("todo_list_id");
        });
        base.OnModelCreating(modelBuilder);
    }
}