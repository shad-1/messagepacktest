using MessagePack;

var AllowSpecificOrigins = "_allowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200");
                      });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(AllowSpecificOrigins);

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = new WeatherForecast();
    byte[] resp = MessagePackSerializer.Serialize(forecast);
    return resp;
})
.WithName("GetWeatherForecast");

app.Run();

[MessagePackObject]
public class WeatherForecast
{
    public WeatherForecast()
    {
        Date = DateTime.Now;
        Summary = "balmy";
        TemperatureC = 28;
    }
    [Key(0)]
    public DateTime Date { get; set; }
    [Key(1)]
    public string Summary { get; set; }
    [Key(2)]
    public int TemperatureC { get; set; }
    [IgnoreMember]
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
