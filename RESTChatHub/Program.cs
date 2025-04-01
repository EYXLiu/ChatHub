using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RESTChatHub.Data;
using RESTChatHub.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ChatHistory>(options => 
    options.UseSqlite("Data Source=chat.db")
);

builder.Services.AddDbContext<UserHistory>(options => 
    options.UseSqlite("Data Source=user.db")
);

builder.Services.AddSingleton<WebSocketConnectionManager>();
builder.Services.AddSingleton<WebSocketHandler>();
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext= scope.ServiceProvider.GetRequiredService<ChatHistory>();
    dbContext.Database.Migrate();
    var userContext = scope.ServiceProvider.GetRequiredService<UserHistory>();
    userContext.Database.Migrate();
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
