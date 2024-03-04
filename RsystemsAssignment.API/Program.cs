using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RsystemsAssignment.Business.Interfaces;
using RsystemsAssignment.Business.Services;
using RsystemsAssignment.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Rsystems API",
        Version = "v1",
        Description = "It will shows multiple api's",
    });

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

IConfiguration configuration = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json")
       .Build();

var shardMapManagerConnectionString = configuration.GetConnectionString("ShardMapManagerConnection");
//var shardMapManager = ShardMapManagerFactory.GetSqlShardMapManager(shardMapManagerConnectionString, ShardMapManagerLoadPolicy.Lazy);
//builder.Services.AddSingleton<ShardMapManager>(shardMapManager);

ShardMapManager shardMapManager;
bool shardMapManagerExists = ShardMapManagerFactory.TryGetSqlShardMapManager(
                                    shardMapManagerConnectionString,
                                    ShardMapManagerLoadPolicy.Lazy,
                                    out shardMapManager);

if (shardMapManagerExists)
{
    Console.WriteLine("Shard Map Manager already exists");
}
else
{
    // Create the Shard Map Manager.
    ShardMapManagerFactory.CreateSqlShardMapManager(shardMapManagerConnectionString);
    Console.WriteLine("Created SqlShardMapManager");

    shardMapManager = ShardMapManagerFactory.GetSqlShardMapManager(
        shardMapManagerConnectionString,
        ShardMapManagerLoadPolicy.Lazy);
}

RangeShardMap<int>shardMap;
bool shardMapExists = shardMapManager.TryGetRangeShardMap("ShardDB01", out shardMap);

if (shardMapExists)
{
    Console.WriteLine($"Shard Map {shardMap.Name} already exists");
}
else
{
    // The Shard Map does not exist, so create it
    shardMap = shardMapManager.CreateRangeShardMap<int>("ShardDB01");
    Console.WriteLine($"Created Shard Map {shardMap.Name}");
}

builder.Services.Configure<ConfigurationSettings>(configuration.GetSection("ConfigurationSettings"));


builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IAppointmentService, AppointmentService>();

builder.Services.AddDbContext<ApplicationContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("ShardMapManagerConnection")));

builder.Services.AddDbContext<ApplicationContext>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.UseCors("CorsPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Rsystems API v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
