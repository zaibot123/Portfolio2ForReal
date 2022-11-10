using DataLayer;
using AutoMapper;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<Interfaces, DataService>();

var app = builder.Build();

app.MapControllers();

app.Run();