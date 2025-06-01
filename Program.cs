using Microsoft.EntityFrameworkCore;
using OrgManager_API.Interfaces;
using OrgManager_API.Model;
using OrgManager_API.Reposatories;
using OrgManager_API.Reposatorties;

namespace OrgManager_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("cs");
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Registering a Service So that every time someone asks for IDepartmentReposatory the program sends DepartmentReposatory
            builder.Services.AddScoped<IDepartmentReposatory, DepartmentReposatory>();
            builder.Services.AddScoped<IEmployeeReposatory, EmployeeReposatory>();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}