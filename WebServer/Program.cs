using DataLayer;
using AutoMapper;
using DataLayer.DataServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IActorDataService, ActorDataService>();
builder.Services.AddSingleton<IMovieDataService, MovieDataService>();
builder.Services.AddSingleton<ILoginDataService, LoginDataService>();

var app = builder.Build();

app.MapControllers();

app.Run();