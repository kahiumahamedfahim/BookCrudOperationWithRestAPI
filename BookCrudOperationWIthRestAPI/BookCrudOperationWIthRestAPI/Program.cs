using BookCrudOperationWIthRestAPI.DataAcess;
using BookCrudOperationWIthRestAPI.Repository;
using BookCrudOperationWIthRestAPI.Repository.Interfaces;
using BookCrudOperationWIthRestAPI.Services;
using BookCrudOperationWIthRestAPI.Services.Interfaces;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.RateLimiting;

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
//API rate limit fixed window

builder.Services.AddRateLimiter(rateLimitOption =>
{
    rateLimitOption.AddFixedWindowLimiter(policyName: "Fixed", options =>
    {
        options.PermitLimit = 2;
        options.Window=TimeSpan.FromSeconds(5);
        options.QueueLimit = 2;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
    rateLimitOption.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});
//Api rate limiting sliding window
builder.Services.AddRateLimiter(rateLimitOption =>
{
    rateLimitOption.AddSlidingWindowLimiter(policyName: "sliding", options =>
    {
        options.PermitLimit= 4;
        options.Window=TimeSpan.FromSeconds(20);
        //options.QueueLimit = 2;
        options.SegmentsPerWindow = 4;
        //options.QueueProcessingOrder= QueueProcessingOrder.OldestFirst;
        
    }

    );
    rateLimitOption.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

//API rate limit Concurrent 
builder.Services.AddRateLimiter(rateLimitOption =>
{
    rateLimitOption.AddConcurrencyLimiter(policyName: "Concurrent", options =>
    {
        options.PermitLimit = 3;
        options.QueueLimit = 2;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;

    });

    rateLimitOption.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});
builder.Services.AddHttpClient();

var app = builder.Build();
if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
