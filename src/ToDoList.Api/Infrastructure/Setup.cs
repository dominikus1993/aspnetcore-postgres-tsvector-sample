using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Infrastructure.EntityFramework;

namespace ToDoList.Api.Infrastructure;

public static class Setup
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContextPool<ToDoDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("ToDoList"), optionsBuilder =>
            {
                
            }).UseSnakeCaseNamingConvention();
        });
        return builder;
    }
}