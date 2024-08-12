using App.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.AddCors();
builder.Services.ConfigureApiControllers();
builder.Services.AddDefaultConfigurations();
builder.Services.AddMediators();
builder.Services.AddValidators();
builder.Services.AddServicesAndRepository();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader());

app.AddRemoveResponseHeaders();
app.UseForwardedHeaders();
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.RunPendingMigrations();
app.Run();
