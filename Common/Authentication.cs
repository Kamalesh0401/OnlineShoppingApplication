using Microsoft.IdentityModel.JsonWebTokens; // For JwtSecurityTokenHandler
using Microsoft.IdentityModel.Tokens;         // For SecurityTokenDescriptor, SymmetricSecurityKey, TokenValidationParameters, etc.
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Common
{
    public class Authentication
    {
        private readonly static string secretKey = "JsonWebTokenTestAdminUser@123456789"; // Same secret key used for signing the token
        private readonly static string issuer = "MyIssuer";
        private readonly static string audience = "MyAudience";

        // Method to generate JWT Token
        public static string GenerateJwtToken(string userId, string userName, string userRole)
        {
            var data = Encoding.UTF8.GetBytes(secretKey);
            var securityKey = new SymmetricSecurityKey(data);

            var claims = new Dictionary<string, object>
            {
                [ClaimTypes.Name] = userName,
                [ClaimTypes.Role] = userRole,
                [ClaimTypes.Sid] = userId
            };

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Claims = claims,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var handler = new JwtSecurityTokenHandler(); // Use JwtSecurityTokenHandler from Microsoft.IdentityModel.JsonWebTokens
            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
        // Method to verify JWT Token
        public static bool VerifyJwtToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler(); // Use JwtSecurityTokenHandler from Microsoft.IdentityModel.JsonWebTokens
                var key = Encoding.UTF8.GetBytes(secretKey);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                // Validate the token
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // If we reach here, the token is valid
                return true;
            }
            catch (Exception ex)
            {
                // If validation fails, catch the exception and return false
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return false;
            }
        }

        public static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }

        public static string Base64Encode(string? plainText, bool EncodeRequired)
        {
            if (EncodeRequired)
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }
            else
            {
                return plainText;
            }
        }
        public static string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
            return hashedPassword;
        }
    }
}
