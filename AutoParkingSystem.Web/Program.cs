using AutoParkingSystem.Core.Services;
using AutoParkingSystem.Infrastructure.Services;
using AutoParkingSystem.Repository;
using Microsoft.EntityFrameworkCore;

namespace AutoParkingSystem.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //ע�����ݿ�������
            builder.Services.AddDbContext<ParkDbContext>(
                options =>
                {
                    options.UseMySql(builder.Configuration.GetConnectionString("AutoParkingSysConn"), new MySqlServerVersion(new Version(8, 0, 26)));

                });

            // ע��PLCͨ�ŷ�������ע�����SiemensPlcCommunicationService
            builder.Services.AddSingleton<IPlcCommunicationService, SiemensPlcCommunicationService>();

            // ע��ҵ���߼�����
            builder.Services.AddScoped<IParkingService, ParkingService>();
            builder.Services.AddScoped<IParkPositionRepository, ParkPositionRepository>();

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