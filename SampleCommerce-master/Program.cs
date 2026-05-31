
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SampleCommerce.Context;
using SampleCommerce.Context.Mappings;
using SampleCommerce.Repositories;
using SampleCommerce.Services;
using System.Text;

namespace SampleCommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("BlazorFrontend", policy =>
                    policy.WithOrigins("https://localhost:7285", "http://localhost:5121")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });

            #region JWT Authentication
            var jwtKey = builder.Configuration["Jwt:Key"]!;
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });
            #endregion

            #region adds dbContext
            string connectionString = builder.Configuration.GetSection("ConnectionString").Value!;
            builder.Services.AddDbContext<EcommerceDbContext>(
                options => options.UseSqlServer(connectionString,
                    sql => sql.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null)));
            #endregion

            #region adds Repos and services
            builder.Services.AddScoped<UserRepo>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<JwtService>();
            builder.Services.AddScoped<AddressRepo>();
            builder.Services.AddScoped<AddressService>();
            builder.Services.AddScoped<OrdersRepo>();
            builder.Services.AddScoped<OrdersService>();
            builder.Services.AddScoped<ProductRepo>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ReviewsRepo>();
            builder.Services.AddScoped<ReviewService>();
            builder.Services.AddScoped<SkusRepo>();
            builder.Services.AddScoped<SkuService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            #endregion

            MapsterConfig.Configure();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("BlazorFrontend");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
