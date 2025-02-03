using CartonCaps.WebApi.IoC;
using CartonCaps.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);


// TODO: Verify working
builder.Services.AddWebDependencies(builder.Configuration);


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
app.UseMiddleware<GlobalErrorHandlingMiddleware>();

app.Run();

public partial class Program { }