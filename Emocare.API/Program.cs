using Emocare.API.Middlewares;
using Emocare.Application.Services;
using Emocare.Infrastructure.Extensions.Auth;
using Emocare.Infrastructure.Extensions.DI;
using Emocare.Infrastructure.Extensions.Integrations.CloudinaryExtensions;
using Emocare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using VagueVault.Backend.Middleware;
namespace Emocare.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.Services.AddSwaggerWithJwt();
            builder.Services.AddSwaggerGen();
       
            builder.Services.ConfigureMapProfile(); 
            builder.Services.ConfigureRepository();
            builder.Services.ConfigureHelper(); 
            builder.Services.AddCloudinary(builder.Configuration);
            builder.Services.ConfigureServices();
            builder.Services.ConfigureDbContext(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowSpecificOrigin");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseUserIdMiddleware();  
            app.UseAuthorization();
            app.UseExceptionMiddleware();


            app.MapControllers();
            app.MapHub<ChatHub>("/chathub");

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}
