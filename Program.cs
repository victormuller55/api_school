using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.Infraestrutura;
using WebApi.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var key = "Ab3Z9kM2pQ7xT4Na8LR5cD6EfJ1SYm0HUwBV9tKePG3Zd2CA7n";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key)
        )
    };
});

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ConnectionContext>(options =>
    options.UseNpgsql(
        "Server=localhost;" +
        "Port=5432;Database=db_employeer;" +
        "User Id=postgres;" +
        "Password=root;")
);

builder.Services.AddTransient<IStudentRepository, EstudanteRepository>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WebApi",
        Version = "v1",
        Description = "API com cadastro de employees"
    });
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ConnectionContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1");
});
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();

app.Run();
