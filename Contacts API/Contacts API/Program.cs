using Contacts_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Contacts_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            builder.Services.AddMvc();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddDbContext<ContactsAPIDbContext>(options => options.UseInMemoryDatabase("ContactsDb"));
            builder.Services.AddDbContext<ContactsAPIDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsApiConnectionString")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins",
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:7235/index.html");
                    });
            });

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseDefaultFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Contacts}/{action=GetContacts}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=GetAccount}/{id?}");

            app.UseCors("_myAllowSpecificOrigins");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            //app.MapControllers();

            app.Run();
        }
    }
}