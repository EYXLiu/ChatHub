using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ChatHub.Data;
using ChatHub.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ChatHistory>(options => 
    options.UseSqlite("Data Source=chat.db")
);

builder.Services.AddScoped<WebSocketConnectionManager>();
builder.Services.AddScoped<WebSocketHandler>();
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext= scope.ServiceProvider.GetRequiredService<ChatHistory>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseWebSockets();

app.MapGet("/", () => 
{
    return new { data = "Hello World"}; 
})
.WithName("Get");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); 

app.Run();
