using BankingPortal.API.Data;
using BankingPortal.API.DTOs;
using BankingPortal.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankingPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly AwsSecretsService _awsSecretsService;

        private readonly BankingDbContext _context;


        // ======================================================
        // Constructor
        // ======================================================

        public AuthController(
            IConfiguration configuration,
            AwsSecretsService awsSecretsService,
            BankingDbContext context)
        {
            _configuration = configuration;
            _awsSecretsService = awsSecretsService;
            _context = context;
        }


        // ======================================================
        // LOGIN API
        // POST: /api/Auth/login
        // ======================================================

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequest request)
        {
            // ==================================================
            // 1. Validate username/password
            // ==================================================

            var user = _context.Users
                .FirstOrDefault(u =>
                    u.Username == request.Username &&
                    u.Password == request.Password);

            if (user == null)
            {
                return Unauthorized(
                    "Invalid username or password");
            }


            // ==================================================
            // 2. Get JWT Secret
            // ==================================================

            // Currently returns the hardcoded secret
            // from AwsSecretsService.

            var jwtSecret =
                await _awsSecretsService
                    .GetJwtSecretAsync();


            // ==================================================
            // 3. Create Claims
            // ==================================================

            var claims = new[]
            {
                new Claim(
                    ClaimTypes.Name,
                    user.Username
                ),

                new Claim(
                    ClaimTypes.Role,
                    user.Role
                )
            };


            // ==================================================
            // 4. Create Security Key
            // ==================================================

            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        jwtSecret
                    )
                );


            // ==================================================
            // 5. Create Signing Credentials
            // ==================================================

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                );


            // ==================================================
            // 6. Create JWT Token
            // ==================================================

            var token =
                new JwtSecurityToken(
                    issuer:
                        _configuration["Jwt:Issuer"],

                    audience:
                        _configuration["Jwt:Audience"],

                    claims:
                        claims,

                    expires:
                        DateTime.UtcNow.AddMinutes(
                            Convert.ToDouble(
                                _configuration[
                                    "Jwt:ExpirationMinutes"
                                ]
                            )
                        ),

                    signingCredentials:
                        credentials
                );


            // ==================================================
            // 7. Convert JWT to String
            // ==================================================

            var jwt =
                new JwtSecurityTokenHandler()
                    .WriteToken(token);


            // ==================================================
            // 8. Return JWT
            // ==================================================

            return Ok(new
            {
                token = jwt
            });
        }
    }
}