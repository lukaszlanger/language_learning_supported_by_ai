using LanguageLearningAI.Domain.Entities;
using LanguageLearningAI.Service;
using LanguageLearningAI.Service.Repositories;
using LanguageLearningAI.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace LanguageLearningAI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy
                            .WithOrigins("http://localhost:8100")
                            .WithOrigins("http://localhost:8101")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            // Register repositories
            builder.Services.AddScoped<PhraseRepository>();
            builder.Services.AddScoped<LessonRepository>();
            builder.Services.AddScoped<QuizRepository>();

            // Register services
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<PhraseService>();
            builder.Services.AddScoped<LessonService>();
            builder.Services.AddScoped<QuizService>();
            builder.Services.AddScoped(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var httpClient = provider.GetRequiredService<HttpClient>();
                return new OpenAIService(configuration, httpClient);
            });

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("LanguageLearningAI.Service")));

            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<SignInManager<User>>();


            // Add controllers
            builder.Services.AddControllers();

            // Configure Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LanguageLearningAPI", Version = "v1" });
                c.IncludeXmlComments(".\\documentation.xml");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowFrontend");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}