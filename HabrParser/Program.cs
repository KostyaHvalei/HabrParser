using HabrParser.Contracts;
using HabrParser.Data;
using HabrParser.Extensions;
using HabrParser.Services;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFeedHttpClient(builder.Configuration);
builder.Services.AddFeedParsingService();
builder.Services.AddArticlesService();
builder.Services.AddRepositories();
builder.Services.ConfigureHangfire(builder.Configuration);

builder.Services.ConfigureApplicationContext(builder.Configuration);

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

app.ConfigureExceptionHandler();

app.UseHangfireDashboard();
app.MapHangfireDashboard();

app.UseAuthorization();

app.MapControllers();

app.Run();
