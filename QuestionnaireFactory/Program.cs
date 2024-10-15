using QuestionnaireFactory.Entities;
using QuestionnaireFactory.Services.ListeCandidat;

namespace QuestionnaireFactory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Ajouter la configuration du DbContext
            builder.Services.AddDbContext<QuestionnairefactorydbContext>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Ajouter le service ListeCandidat avec une durée de vie Scoped (une instance par requête HTTP)
            builder.Services.AddScoped<ListeCandidat>();

            //Add loggin check

            builder.Services.AddLogging(options =>
                options.AddAzureWebAppDiagnostics()
                );

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
