using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiBocataAPI.Helpers
{
	public static class JwtSecurityTokenHelper
    {
		public static string BuildToken(
			string JwtKey, 
			Shopkeeper empleado)
		{
			return BuildToken(JwtKey, empleado.Id.ToString(), empleado.Email.ToString(), empleado.IdStore.ToString());
		}

		public static string BuildToken(
			string JwtKey, 
			Client cliente)
		{
			return BuildToken(JwtKey, cliente.Id.ToString(), cliente.Email.ToString());
		}

		private static string BuildToken(
			string JwtKey,
			string id,
			string email,
			string idTienda = "")
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(JwtKey);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Email, email),
					new Claim(ClaimTypes.NameIdentifier, id),
				})
			,
				Expires = DateTime.UtcNow.AddMinutes(1440),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}

		//private static string BuildToken(
		//	string JwtKey,
		//	string id,
		//	string email,
		//	string idTienda = "")
		//{
		//	var claims = new[] {
		//					new Claim(JwtRegisteredClaimNames.NameId, id),
		//					new Claim(JwtRegisteredClaimNames.Email, email),
		//					new Claim("IdTienda", idTienda),
		//					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		//						};


		//	var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
		//	var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		//	var token = new JwtSecurityToken(
		//	  null,
		//	  null,
		//	  expires: DateTime.Now.AddDays(7),
		//	  claims: claims,
		//	  signingCredentials: creds);

		//	return new JwtSecurityTokenHandler().WriteToken(token);
		//}
	}
}
