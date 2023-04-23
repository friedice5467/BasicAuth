using BasicAuth.DAL.Repo.Abstract;
using BasicAuth.DAL.Repo;
using BasicAuth.DAL;
using BasicAuth.Service.Abstract;
using BasicAuth.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace BasicAuth
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            using var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("BasicAuth.appsettings.json");
            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
            builder.Configuration.AddConfiguration(config);

            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddSingleton<IAppStateService, AppStateService>();


            builder.Services.AddDbContext<IdentityDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityDbConnection")));

            builder.Services.AddLogging();

            return builder.Build();
        }
    }
}
