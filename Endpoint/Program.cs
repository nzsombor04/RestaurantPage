using Data;
using Logic.Helper;
using Logic.Logic;
using Microsoft.EntityFrameworkCore;

namespace Endpoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddTransient(typeof(Repository<>));
            builder.Services.AddTransient<RestaurantLogic>();
            builder.Services.AddTransient<DtoProvider>();
            builder.Services.AddTransient<ItemLogic>();
            builder.Services.AddTransient<ReviewLogic>();


            builder.Services.AddDbContext<RestaurantPageContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RestaurantPageDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");
                options.UseLazyLoadingProxies();
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
