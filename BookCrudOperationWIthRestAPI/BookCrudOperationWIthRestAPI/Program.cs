using BookCrudOperationWIthRestAPI.DataAcess;
using BookCrudOperationWIthRestAPI.Repository;
using BookCrudOperationWIthRestAPI.Repository.Interfaces;
using BookCrudOperationWIthRestAPI.Services;
using BookCrudOperationWIthRestAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//dependence Injection
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookServices,BookServices>();
//Database Connection
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
