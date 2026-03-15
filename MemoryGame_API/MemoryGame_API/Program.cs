using MemoryGame_API.IMappers;
using MemoryGame_API.IRepositories;
using MemoryGame_API.IServices;
using MemoryGame_API.Mappers;
using MemoryGame_API.Models;
using MemoryGame_API.Repositories;
using MemoryGame_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddSingleton<IDifficultiesMapper, DifficultiesMapper>();
builder.Services.AddSingleton<IAuthMapper, AuthMapper>();
builder.Services.AddSingleton<IGameResultsMapper, GameResultsMapper>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IStatisticalMapper, StatisticalMapper>();

builder.Services.AddScoped<IDifficultiesService, DifficultiesService>();
builder.Services.AddScoped<IDifficultiesRepository, DifficultiesRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGameResultsService, GameResultsService>();
builder.Services.AddScoped<IGameResultsRepository, GameResultsRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("RenderPolicy", policy =>
    {
        policy.WithOrigins("https://memorygame-xnn6.onrender.com", "http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var config = services.GetRequiredService<IConfiguration>();

    string connString = config.GetConnectionString("DefaultConnection") ?? "NULL";
    Console.WriteLine($"[DEBUG] Connection String: {connString}");

    try
    {
        Console.WriteLine("[DEBUG] Avvio migrazioni...");
        context.Database.Migrate();
        Console.WriteLine("[DEBUG] Migrazioni completate con successo.");

        // Verifica se le tabelle esistono davvero
        var canConnect = context.Database.CanConnect();
        Console.WriteLine($"[DEBUG] Il database è raggiungibile: {canConnect}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[ERROR] Errore durante la migrazione: {ex.Message}");
    }
}

app.UseCors("RenderPolicy");

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapFallbackToFile("index.html");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();