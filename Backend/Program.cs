using Backend.Contexts;
using Backend.Entities;
using Backend.Repositories.Interfaces;
using Backend.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Backend.Services.Interfaces;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMe",
        builder => builder
            .WithOrigins("http://localhost:5299")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>
    (DbContextOptions => DbContextOptions.UseSqlite(
        builder.Configuration["ConnectionStrings:testAppDBConnectionString"]));
builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<DataContext>()
    .AddApiEndpoints();
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

app.MapIdentityApi<ApplicationUser>();
app.MapControllers();

app.Run();
