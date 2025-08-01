using Emocare.Application.Interfaces;
using Emocare.Application.Mappings;
using Emocare.Application.Services;
using Emocare.Domain.Interfaces.Extension;
using Emocare.Domain.Interfaces.Helper.AiChat;
using Emocare.Domain.Interfaces.Helper.Auth;
using Emocare.Domain.Interfaces.Repositories;
using Emocare.Domain.Interfaces.Repositories.Chat;
using Emocare.Domain.Interfaces.Repositories.Habits;
using Emocare.Domain.Interfaces.Repositories.User;
using Emocare.Infrastructure.Extensions.Integrations.CloudinaryExtension;
using Emocare.Infrastructure.Persistence;
using Emocare.Infrastructure.Repositories;
using Emocare.Infrastructure.Repositories.Chat;
using Emocare.Infrastructure.Repositories.Habits;
using Emocare.Infrastructure.Repositories.User;
using Emocare.Shared.Helpers.Auth;
using Emocare.Shared.Helpers.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Emocare.Infrastructure.Extensions.DI
{
    public static class ServiceExtension
    {
        public static void ConfigureDbContext(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        }
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));    
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IPsychologistRepository, PsychologistRepository>();
            services.AddScoped<IPasswordHistoryRepo, PasswordHistoryRepo>();
            services.AddScoped<IWellnessTaskRepository, WellnessTaskRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<IChatSessionRepository,ChatSessionRepository>();
            services.AddScoped<IJournalEntryRepository, JournalEntryRepository>();
            services.AddScoped<IHabitRepository,HabitRepository>();
            services.AddScoped<IHabitHabitCompletionRepository, HabitHabitCompletionRepository>();
            services.AddScoped<IHabitCategoryRepository, HabitCategoryRepository>();    
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IAuthenticationServices,AuthenticationServices>();
            services.AddScoped<IUserManageServices,UserManageServices>();   
            services.AddScoped<IUserService,UserServices>();    
            services.AddScoped<IWellnessTaskService, WellnessTaskService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IReflectionServices, ReflectionServices>();
            services.AddScoped<IPsychologistServices,PsychologistServices>();
            services.AddScoped<IHabitServices,IHabitServices>();    
        }

        public static void ConfigureHelper(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher,PasswordHasher>();
            services.AddScoped<IPasswordValidator, PasswordValidator>();
            services.AddScoped<IJwtHelper,JwtHelper>();
            services.AddSignalR();
            services.AddHttpClient<IOpenRouterStreamService,OpenRouterStreamService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserFinder, UserFinder>();

        }

        public static void ConfigureMapProfile(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfile));
        }
    }
}
