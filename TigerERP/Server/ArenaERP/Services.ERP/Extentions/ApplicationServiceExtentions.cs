using DatabaseContext;
using Microsoft.EntityFrameworkCore;
namespace Services.ERP.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAutoMapper(typeof(AutoMappingProfile).Assembly);

            services.AddDbContext<DatabaseContextDb>(options =>

            options.UseSqlServer(configuration.GetConnectionString("Connection")), ServiceLifetime.Scoped);
            //services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
            //services.AddSingleton<IMailer, Mailer>();
            //services.AddHangfire(x => x.UseSqlServerStorage("Server=DESKTOP-HB29V55\\BAPPY;Database=DaddingApps;"));
            //services.AddHangfireServer();
            return services;
        }
    }
}
