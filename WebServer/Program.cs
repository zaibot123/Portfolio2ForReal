using DataLayer;
using AutoMapper;
using DataLayer.DataServices;
using DataLayer.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IActorDataService, ActorDataService>();
builder.Services.AddSingleton<IMovieDataService, MovieDataService>();
builder.Services.AddSingleton<IuserDataService, UserDataService>();

var app = builder.Build();

app.UseCors(
    options =>
    options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();