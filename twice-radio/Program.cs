var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(cors =>
{
  cors.AddPolicy("default", opt =>
  {
    opt.AllowAnyHeader();
    opt.AllowAnyMethod();
    opt.AllowAnyOrigin();
  });
});

var app = builder.Build();

app.UseCors("default");

app.MapControllers();

app.Run();
