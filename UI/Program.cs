using BLL.Services;
using DLL.Context;
using DLL.Repositories;
using DLL.Repositories.Interfaces;
using Domain.Entities;
using FluentValidation;
using UI.Validators.Film;
using UI.Validators.User;
using Microsoft.EntityFrameworkCore;
using UI.Validators.Comment;
using UI.MapperProfile;
using BLL.MapperProfile;

namespace homework10WEB_API
{
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

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<UserService>();

            builder.Services.AddTransient<IRepository<Film>, FilmRepository>();
            builder.Services.AddTransient<FilmService>();

            builder.Services.AddTransient<IRepository<Comment>, CommentRepository>();
            builder.Services.AddTransient<CommentService>();

            builder.Services.AddValidatorsFromAssemblyContaining<FilmCreateRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<FilmUpdateRequestValidator>();

            builder.Services.AddValidatorsFromAssemblyContaining<UserLoginRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UserUpdateRequestValidator>();

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
}
