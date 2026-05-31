using BlazorSampleCommerce.Components;
using BlazorSampleCommerce.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BlazorSampleCommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddScoped<TokenStore>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<SearchService>();
            builder.Services.AddScoped<CartService>();
            builder.Services.AddScoped<AuthTokenHandler>();
            builder.Services.AddScoped(sp =>
            {
                var handler = sp.GetRequiredService<AuthTokenHandler>();
                handler.InnerHandler = new HttpClientHandler();
                return new HttpClient(handler)
                {
                    BaseAddress = new Uri("https://localhost:7082/")
                };
            });

            builder.Services.AddScoped<LocalStorageService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
