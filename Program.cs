

using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.DatabaseContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Adding DbContext Services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Adding Swagger Services
builder.Services.AddEndpointsApiExplorer();   //Configure the endpoints of the API
builder.Services.AddSwaggerGen();        // To generate the Open API Specification and configures the Swagger to generate documentation for API's Endpoints

//Adding CORS Services
builder.Services.AddCors(options  =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
        .WithHeaders("Authorization", "content-type", "accept", "origins")
        .WithMethods("POST", "GET", "PUT", "DELETE");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHsts();
app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();


app.Run();
