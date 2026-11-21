using BLL.MapperProfile;
using BLL.Services;
using DLL.Context;
using DLL.Repositories;
using DLL.Repositories.Interfaces;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UI.MapperProfile;
using UI.Validators.Comment;
using UI.Validators.Film;
using UI.Validators.FilmSession;
using UI.Validators.User;

namespace homework10WEB_API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", policy =>
            {
                policy.WithOrigins("http://localhost:4200") // here change on your front-end url
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("def")));

        builder.Services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 6;

            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_.-";

            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
        }).AddEntityFrameworkStores<AppDbContext>().AddSignInManager();

        builder.Services.AddTransient<AuthService>();

        builder.Services.AddTransient<IFilmSessionRepository, FilmSessionRepository>();
        builder.Services.AddTransient<FilmSessionService>();

        builder.Services.AddTransient<IFilmRepository, FilmRepository>();
        builder.Services.AddTransient<FilmService>();

        builder.Services.AddTransient<IRepository<Comment>, CommentRepository>();
        builder.Services.AddTransient<CommentService>();

        builder.Services.AddValidatorsFromAssemblyContaining<FilmCreateRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<FilmUpdateRequestValidator>();

        builder.Services.AddValidatorsFromAssemblyContaining<FilmSessionCreateRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<FilmSessionUpdateRequestValidator>();

        builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UserLoginRequestValidator>();

        builder.Services.AddValidatorsFromAssemblyContaining<CommentCreateValidator>();

        builder.Services.AddAutoMapper(opt =>
        {
            opt.AddProfile<ReqResToDtoProfile>();
            opt.AddProfile<DtoToEntityProfile>();
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "Really cool cinema web site");
            });
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowSpecificOrigin");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
