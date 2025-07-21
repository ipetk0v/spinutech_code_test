using SpinutechCodeTest.Services;

namespace SpinutechCodeTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register custom services
            builder.Services.AddScoped<INumberToWordsService, NumberToWordsService>();
            builder.Services.AddScoped<IPokerHandService, PokerHandService>();
            builder.Services.AddScoped<ISpiralService, SpiralService>();
            builder.Services.AddScoped<IGameOfLifeService, GameOfLifeService>();
            builder.Services.AddScoped<ITemplateEngineService, TemplateEngineService>();
            builder.Services.AddScoped<IPalindromeService, PalindromeService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
