using GameHub.Common.Auth;
using Microsoft.EntityFrameworkCore;
using UserService.Data;

namespace UserService.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserServiceCore(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.Configure<JwtSettings>(config.GetSection("Jwt"));
        services.AddScoped<AuthService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                var key = config["Jwt:Key"]!;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key))
                };
            });

        services.AddAuthorization();
        services.AddControllers();

        return services;
    }
}