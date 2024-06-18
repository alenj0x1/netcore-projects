using cache.Dependencies;
using cache.Dependencies.Contract;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
