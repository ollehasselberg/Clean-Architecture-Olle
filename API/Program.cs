using ApplicationLayer.TodoItems.Commands;
using DomainLayer.Interfaces;
using InfrastructureLayer.Repositories;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Repository Pattern: kopplar Domain-interfacen till sina EF Core-implementationer i Infrastructure
            builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            builder.Services.AddScoped<ITodoListRepository, TodoListRepository>();

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                    // Förhindrar krasch vid TodoItem <-> TodoList cirkelreferens (EF Core navigation fixup)
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

            // Registrerar MediatR och skannar ApplicationLayer efter alla Command/Query-handlers
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateTodoItemCommand).Assembly));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // EF Core DbContext, kopplad mot SQL Server-connection stringen i appsettings.json
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                     builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

    }
}
