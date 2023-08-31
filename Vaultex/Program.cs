using Vaultex.Factories;
using Vaultex.Factories.ImportStrategies;
using Vaultex.Factories.Interfaces;
using Vaultex.Repository;
using Vaultex.Services;
using Vaultex.Services.Interfaces;
using Vaultex.ValueSets;

var builder = WebApplication.CreateBuilder(args);

// Configuration
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//Repository
builder.Services.AddSingleton<IPostgresRepository, PostgresRepository>();

//Factories
builder.Services.AddTransient<IImportStrategy, ImportFromExcel>();
builder.Services.AddTransient<IImportStrategyFactory, ImportStrategyFactory>();

//Services
builder.Services.AddSingleton<IImportService, ImportService>();
builder.Services.AddSingleton<IVaultexDataService, VaultexDataService>();

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
app.UseCors();

app.Run();