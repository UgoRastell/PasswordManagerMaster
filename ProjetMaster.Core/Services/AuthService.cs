using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjetMaster.Core.DTOs;
using ProjetMaster.Core.Interfaces;
using ProjetMaster.Core.Models;
using ProjetMaster.Core.Models.ProjetMaster.Core.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProjetMaster.Core.Services
{
	public class AuthService
	{
		private readonly IApplicationDbContext _context;
		private readonly IConfiguration _configuration;

		public AuthService(IApplicationDbContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		public (bool Success, string Message) Register(RegisterRequestDto request)
		{
			var existingUser = _context.Users.FirstOrDefault(u => u.Email == request.Email);
			if (existingUser != null)
				return (false, "Identifiants pas bon.");

			var hashedPassword = HashPassword(request.Password);

			var newUser = new UsersModel
			{
				Email = request.Email,
				MotDePasse = hashedPassword,
				DateInscription = DateTime.UtcNow
			};

			_context.Users.Add(newUser);
			_context.SaveChanges();

			return (true, "Inscription réussie.");
		}





		public (bool Success, string Token, string Message, UserResponseDto User) Login(string email, string password)
		{
			var user = _context.Users.FirstOrDefault(u => u.Email == email);
			if (user == null || !VerifyPassword(password, user.MotDePasse))
				return (false, null, "Identifiants invalides.", null);

			var token = GenerateJwtToken(user);

			var userDto = new UserResponseDto
			{
				Id = user.Id.ToString(),
				Email = user.Email,
			};

			return (true, token, "Connexion réussie.", userDto);
		}

		private string HashPassword(string password)
		{
			using var sha256 = SHA256.Create();
			var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
			return Convert.ToBase64String(hashedBytes);
		}

		private bool VerifyPassword(string inputPassword, string storedPassword)
		{
			var hashedInput = HashPassword(inputPassword);
			return hashedInput == storedPassword;
		}

		private string GenerateJwtToken(UsersModel user)
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddHours(2),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
