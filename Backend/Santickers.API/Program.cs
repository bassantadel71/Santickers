using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Santickers.API.Middleware;
using Santickers.Application.DependencyInjection;
using Santickers.Infrastructure.Identity.Settings;
using Santickers.Infrastructure.Persistence.Data;
using Santickers.Infrastructure.Persistence.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "Insert your JWT token: Bearer {your token}",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		Scheme = "bearer",
		BearerFormat = "JWT"
	});

	options.AddSecurityRequirement(_ => new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecuritySchemeReference("Bearer"),
			new List<string>()
		}
	});
});

var jwtSettings = builder.Configuration
	.GetSection("Jwt")
	.Get<JwtSettings>()!;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = jwtSettings.Issuer,
			ValidAudience = jwtSettings.Audience,
			IssuerSigningKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(jwtSettings.Key))
		};
	});

builder.Services.AddAuthorization();

var app = builder.Build();

// Global exception handling middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

// Initialize database and seed data
using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider
		.GetRequiredService<ApplicationDbContext>();

	await DbInitializer.InitializeAsync(context);
}

// Swagger
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();