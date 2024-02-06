using System.Reflection.PortableExecutable;
using WebApiCore.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

ClassLibrary1.SQLconn con = new ClassLibrary1.SQLconn();

con.conect(true);
    
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseWebSockets();                            // Aceptar WebSockets
app.Map("/ws", b => {                           //Mapping the ws route
    b.UseMiddleware<ControladorWebSocket>();  // Controlador para WebSockets
});

app.MapControllers();

app.Run();
