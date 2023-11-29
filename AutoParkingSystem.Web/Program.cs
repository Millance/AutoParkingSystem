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
            // ע��ҵ���߼�����
            builder.Services.AddScoped<IParkingService, ParkingService>();

            // ע��PLCͨ�ŷ�������ע�����SiemensPlcCommunicationService
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