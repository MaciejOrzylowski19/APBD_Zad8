using ApBD_Zaj8.Services;
using APBD_Zaj8.Services;
using APBD_Zaj8.Services.Delete;
using APBD_Zaj8.Services.Post;
using APBD_Zaj8.Services.Put;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
Console.WriteLine(builder.Services.Count);

builder.Services.AddScoped<ITripsList, TripsList>();
builder.Services.AddScoped<IClientTripService, ClientTripsService>();
builder.Services.AddScoped<IDeleteFromTrip, DeleteFromTripService>();
builder.Services.AddScoped<IAddClient, AddClientService>();
builder.Services.AddScoped<IAddToTrip, AddToTripService>();


builder.Services.AddOpenApi();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
