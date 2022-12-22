
using System.Data.Entity.Infrastructure;
using Npgsql;
using WebAapi.Configuration;
using WebAapi.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IDbConnectionFactory>(services =>
{
    var configuration = services.GetService<IConfiguration>();
    var connectionString = new NpgsqlConnectionStringBuilder
    {
        Host = configuration.PostgreSqlHost(),
        Port = configuration.PostgreSqlPort(),
        Database = configuration.PostgreSqlDatabase(),
        Username = configuration.PostgreSqlUsername(),
        Password = configuration.PostgreSqlPassword(),
        CommandTimeout = configuration.PostgreSqlCommandTimeout()
    }.ToString();
    
    return new NpgsqlConnectionFactory(connectionString);
});
builder.Services.AddDbContext<DatabaseContext>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();