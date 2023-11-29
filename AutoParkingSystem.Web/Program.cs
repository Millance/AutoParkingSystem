using AutoParkingSystem.Core.Services;
using AutoParkingSystem.Infrastructure.Services;

namespace AutoParkingSystem.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // 注册业务逻辑服务
            builder.Services.AddScoped<IParkingService, ParkingService>();

            // 注册PLC通信服务，这里注册的是SiemensPlcCommunicationService
            builder.Services.AddSingleton<IPlcCommunicationService, SiemensPlcCommunicationService>();

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