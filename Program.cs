using BookApi.Services;
using BooksApi.Models;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// requires using Microsoft.Extensions.Options
builder.Services.Configure<BookstoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(BookstoreDatabaseSettings)));

builder.Services.AddSingleton<IBookstoreDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<BookstoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<BookService>();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.UseMemberCasing());


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
