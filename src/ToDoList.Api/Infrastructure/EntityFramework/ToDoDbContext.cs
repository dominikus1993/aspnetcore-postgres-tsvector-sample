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
            builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(p => p.SearchVector).HasComputedColumnSql(
                "to_tsvector('english', id::text || ' ' || name)", true);
            // builder.HasGeneratedTsVectorColumn(
            //         p => p.SearchVector,
            //         "english",  // Text search config
            //         p => new { p.Id, p.Name})  // Included properties
            //     .HasIndex(p => p.SearchVector)
            //     .HasMethod("GIN"); // Index method on the search vector (GIN or GIS
            builder.HasMany(x => x.Items).WithOne();
        });
        modelBuilder.Entity<ToDoItem>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.IsComplete).IsRequired();
            builder.HasOne<ToDos>().WithMany(x => x.Items).HasForeignKey("todo_list_id");
        });
        base.OnModelCreating(modelBuilder);
    }
    
    public async Task SeedAsync()
    {
        await Database.MigrateAsync();
        
        if (await ToDos.AnyAsync()) return;
        
        var toDos = new List<ToDos>
        {
            new()
            {
                Name = "First List",
                CreatedAt = DateTimeOffset.UtcNow,
                Items = new List<ToDoItem>
                {
                    new()
                    {
                        Name = "First Item",
                        CreatedAt = DateTimeOffset.UtcNow,
                        IsComplete = false
                    }
                }
            }
        };
        await AddRangeAsync(toDos);
        await SaveChangesAsync();
    }
}