using System.Text;
using FacultativeSystem.Api;
using FacultativeSystem.Application.Abstractions;
using FacultativeSystem.Application.Services;
using FacultativeSystem.Domain.Repositories;
using FacultativeSystem.Infrastructure;
using FacultativeSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sieve.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataAccess>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebDatabase")));

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebDatabase")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

builder.Services.AddScoped<IFeedbackGradeService, FeedbackGradeService>();
builder.Services.AddScoped<IFeedbackGradeRepository, FeedbackGradeRepository>();

builder.Services.AddScoped<ITokenRepository, TokenRepository>();

builder.Services.RegisterMappings();

builder.Services.AddSingleton<SieveProcessor>();

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("FacultativeSystem")
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            AuthenticationType = "Jwt",
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["Authorization"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token.Replace("Bearer ", "");
                }
                return Task.CompletedTask;
            }
        };
    });


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseCors("AllowAngularDev");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
