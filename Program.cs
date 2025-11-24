using PersonalExpenseTracker.Data;
using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Repositories;
using AutoMapper;
using PersonalExpenseTracker.Mapping;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register Repositories
builder.Services.AddScoped<IUserRepository, SQLUserRepository>();
builder.Services.AddScoped<IExpenseRepository, SQLExpenseRepository>();

// Add DbContext
builder.Services.AddDbContext<PersonalExpenseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PersonalExpenseConnection"))
);




//Automapper Configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
