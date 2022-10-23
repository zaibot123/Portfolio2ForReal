var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddMvcCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();
app.MapControllers();
app.Run();
