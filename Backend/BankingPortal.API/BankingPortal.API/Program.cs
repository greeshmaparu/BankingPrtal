using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using BankingPortal.API.Data;
using BankingPortal.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


// ======================================================
// JWT SECRET
// ======================================================

// ======================================================
// AWS Secrets Manager - TEMPORARILY DISABLED
// ======================================================
//
// Enable this later when EC2 IAM Role is configured.
//
// var secretsManager = new AmazonSecretsManagerClient();
//
// var secretResponse = await secretsManager.GetSecretValueAsync(
//     new GetSecretValueRequest
//     {
//         SecretId = "bankingportal/prod/jwt"
//     });
//
// using var secretDocument =
//     JsonDocument.Parse(secretResponse.SecretString!);
//
// var jwtSecret = secretDocument
//     .RootElement
//     .GetProperty("JwtSecret")
//     .GetString()!;


// ======================================================
// TEMPORARY HARDCODED JWT SECRET
// ======================================================

// TEMPORARY ONLY
// Do NOT commit this secret to Azure DevOps.
var jwtSecret = "HelloGreeshmaBankingPortal2026!2025";


// ======================================================
// AWS Services
// ======================================================

// AWS Secrets Manager temporarily disabled.
//
// Enable this later:
//
// builder.Services.AddAWSService<IAmazonSecretsManager>();

// AuthController still requires AwsSecretsService.
builder.Services.AddScoped<AwsSecretsService>();


// ======================================================
// JWT Authentication
// ======================================================

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;

        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                // Validate JWT signature
                ValidateIssuerSigningKey = true,

                // Use temporary hardcoded secret
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSecret)
                    ),

                // Validate issuer
                ValidateIssuer = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                // Validate audience
                ValidateAudience = true,

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                // Validate expiration
                ValidateLifetime = true,

                // Map JWT role claim for [Authorize(Roles = "...")]
                RoleClaimType = ClaimTypes.Role,

                NameClaimType = ClaimTypes.Name
            };
    });

builder.Services.AddAuthorization();


// ======================================================
// Controllers
// ======================================================

builder.Services.AddControllers();


// ======================================================
// Database
// ======================================================

builder.Services.AddDbContext<BankingDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration
            .GetConnectionString("DefaultConnection")
    ));


// ======================================================
// Application Services
// ======================================================

builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<GeminiService>();


// ======================================================
// Swagger
// ======================================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile =
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath =
        Path.Combine(
            AppContext.BaseDirectory,
            xmlFile
        );

    options.IncludeXmlComments(xmlPath);
});


// ======================================================
// CORS - Angular
// ======================================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// ======================================================
// Build Application
// ======================================================

var app = builder.Build();


// ======================================================
// HTTP Request Pipeline
// ======================================================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();

    app.UseSwaggerUI();
}


// ======================================================
// HTTPS
// ======================================================

app.UseHttpsRedirection();


// ======================================================
// CORS
// ======================================================

app.UseCors("AngularPolicy");


// ======================================================
// Authentication / Authorization
// ======================================================

app.UseAuthentication();

app.UseAuthorization();


// ======================================================
// Controllers
// ======================================================

app.MapControllers();


// ======================================================
// Start Application
// ======================================================

app.Run();