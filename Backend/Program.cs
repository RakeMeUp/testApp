using Backend.Contexts;
using Backend.Entities;
using Backend.Repositories.Interfaces;
using Backend.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Backend.Services.Interfaces;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMe",
        builder => builder
            .WithOrigins("http://localhost:5299")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// configure jwt access in swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "just put the token here no bearer needed"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddDbContext<DataContext>
    (DbContextOptions => DbContextOptions.UseSqlite(
        builder.Configuration["ConnectionStrings:testAppDBConnectionString"]));
builder.Services.AddAuthorization();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});
builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<DataContext>()
    .AddApiEndpoints()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(Program));
// REPOSITORIES
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionGradeRepository, QuestionGradeRepository>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<IUserTestRepository, UserTestRepository>();
builder.Services.AddScoped<IUserTestResultRepository, UserTestResultRepository>();
// SERVICES
builder.Services.AddScoped<IAGIService, AGIService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<TokenService>();


// Secrets
builder.Configuration.AddJsonFile("appsettings.Secrets.json", optional: true, reloadOnChange: true);



var app = builder.Build();

app.UseCors("AllowMe");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<ApplicationUser>();
app.MapControllers();

app.Run();
