using Redis.OM;
using TodoApi;
using StackExchange.Redis;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
c.EnableAnnotations()
);
builder.Services.AddHostedService<IndexCreationService>();
ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(builder.Configuration["REDIS_CONNECTION_STRING"]);

builder.Services.AddSingleton(new RedisConnectionProvider(connectionMultiplexer));

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
