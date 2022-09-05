
using EmployeeService.Dats;
using Microsoft.AspNetCore.HttpLogging;

using Microsoft.Extensions.Options;
using NLog.Web;
using Timesheets.Models.Options;
using Timesheets.Services;
using Timesheets.Services.Impl;
using Timesheets.Services.lmpl;

namespace Timesheets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configure EF DBContext Service (EmployeeDatabase Database)

            builder.Services.AddDbContext<EmployeeServiceDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["Settings:DatabaseOptions:ConnectionString"]);
            });

            #endregion

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeTypeRepository, EmployeeTypeRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            #region Configure Options

            builder.Services.Configure<LoggerOptions>(options =>
              builder.Configuration.GetSection("Settings:Logger").Bind(options)
            );

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
                logging.RequestHeaders.Add("Authorization");
                logging.RequestHeaders.Add("X-Real-IP");
                logging.RequestHeaders.Add("X-Forwarded-For");
            });

            builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();

            }).UseNLog(new NLogAspNetCoreOptions() { RemoveLoggerFactoryFilter = true });
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwagger();
            }


            app.UseAuthorization();

            app.UseHttpLogging();

            app.MapControllers();

            app.Run();
        }
    }
}