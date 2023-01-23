using HabrParser.Contracts;
using HabrParser.Extensions;
using HabrParser.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFeedHttpClient(builder.Configuration);
builder.Services.AddArticlesService();

builder.Services.AddControllers();

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
